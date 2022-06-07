using BISA.Server.Data.DbContexts;
using BISA.Shared.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Security.Claims;



namespace BISA.Server.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly BisaDbContext _context;

        public AuthService(SignInManager<ApplicationUser> signInManager, IConfiguration configuration, BisaDbContext context)
        {
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _context = context;
        }

        public async Task<string> Login(UserLoginDTO userLogin)
        {
            var signInResult = await _signInManager.PasswordSignInAsync(userLogin.Username, userLogin.Password, false, false);

            if (signInResult.Succeeded)
            {
                var currentUser = await _signInManager.UserManager.FindByNameAsync(userLogin.Username);
                return await CreateToken(currentUser);
            }

            throw new AuthenticationException(signInResult.ToString());
        }

        public async Task<string> Register(UserRegisterDTO userRegister)
        {
            ApplicationUser newUser = new();
            newUser.UserName = userRegister.Username;
            newUser.Email = userRegister.Email;

            var createUser = await _signInManager.UserManager.CreateAsync(newUser, userRegister.Password);

            if (!createUser.Succeeded)
            {
                throw new ArgumentException(createUser.ToString());
            }

            bool addedInBisaDb = await RegisterUserInBisaDb(userRegister);
            if (addedInBisaDb)
            {
                // Login new user to create jwt
                UserLoginDTO userLogin = new UserLoginDTO
                {
                    Username = userRegister.Username,
                    Password = userRegister.Password
                };

                try
                {
                    var signInResult = await Login(userLogin);
                    return signInResult;
                }
                catch (AuthenticationException exception)
                {
                    throw new AuthenticationException(exception.ToString());
                }
            }
            throw new DbUpdateException("Could not add user in Bisa DB");
        }

        private async Task<string> CreateToken(ApplicationUser user)
        {
            var roles = await _signInManager.UserManager.GetRolesAsync(user);
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
               _configuration.GetSection("JWTSettings:SecretForKey").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _configuration["JWTSettings:Issuer"],
                _configuration["JWTSettings:Audience"],
                claims: claims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials: creds
                );


            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        private async Task<bool> RegisterUserInBisaDb(UserRegisterDTO userRegister)
        {
            //get userId from UserDb
            var userInDb = await _signInManager.UserManager.FindByNameAsync(userRegister.Username);

            //convert UserRegisterDTO to UserEntity
            UserEntity newUserinBisDb = new();
            newUserinBisDb.Firstname = userRegister.FirstName;
            newUserinBisDb.Lastname = userRegister.LastName;
            newUserinBisDb.Email = userRegister.Email;
            newUserinBisDb.Username = userRegister.Username;
            newUserinBisDb.UserId = userInDb.Id;

            //add
            var result = await _context.Users.AddAsync(newUserinBisDb);
            await _context.SaveChangesAsync();
            if (result != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
