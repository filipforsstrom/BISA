﻿using BISA.Server.Data.DbContexts;
using BISA.Shared.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
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

        public async Task<ServiceResponseDTO<string>> Login(UserLoginDTO userLogin)
        {
            ServiceResponseDTO<string> serviceResponse = new();
            var signInResult = await _signInManager.PasswordSignInAsync(userLogin.Username, userLogin.Password, false, false);

            if (signInResult.Succeeded)
            {

                var currentUser = await _signInManager.UserManager.FindByNameAsync(userLogin.Username);
                var jwt = await CreateToken(currentUser);
                serviceResponse.Data = jwt;
                serviceResponse.Success = true;
     
                return serviceResponse;
            }
            serviceResponse.Data = null;
            serviceResponse.Success = false;
            serviceResponse.Message = signInResult.ToString();
            return serviceResponse;
        }

        public async Task<ServiceResponseDTO<string>> Register(UserRegisterDTO userRegister)
        {
            ServiceResponseDTO<string> serviceResponse = new();

            ApplicationUser newUser = new();
            newUser.UserName = userRegister.Username;
            newUser.Email = userRegister.Email;

            var result = await _signInManager.UserManager.CreateAsync(newUser, userRegister.Password);
            if (result.Succeeded)
            {
                bool addedInBisaDb = await RegisterUserInBisaDb(userRegister);
                if (addedInBisaDb) 
                {
                    // Login new user to create jwt
                    UserLoginDTO userLogin = new UserLoginDTO
                    {
                        Username = userRegister.Username,
                        Password = userRegister.Password
                    };

                    var signInResult = await Login(userLogin);
                    if (signInResult.Success)
                    {
                        serviceResponse.Data = signInResult.Data;
                        serviceResponse.Success = true;
                        serviceResponse.Message = signInResult.Data;
                        return serviceResponse;
                    }
                    else
                    {
                        serviceResponse.Success = false;
                        serviceResponse.Message = signInResult.Data;
                        return serviceResponse;
                    }
                }
                else
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Could not add user in Bisa DB";
                    return serviceResponse;
                }

                
            }
            serviceResponse.Message = result.ToString();
            serviceResponse.Success = false;
            return serviceResponse;
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
