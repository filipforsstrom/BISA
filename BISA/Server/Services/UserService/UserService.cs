﻿using BISA.Shared.Entities;

namespace BISA.Server.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public UserService(IHttpContextAccessor httpContextAccessor, SignInManager<ApplicationUser> signInManager)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
        }
        public async Task<ServiceResponseDTO<string>> ChangePassword(UserChangePasswordDTO userChangePassword)
        {
            var response = new ServiceResponseDTO<string>();
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var user = await _signInManager.UserManager.FindByIdAsync(userId);

            if (user == null)
            {
                response.Success = false;
                response.Message = "Couldn't find user";
                return response;
            }

            var changePasswordResult = await _signInManager.UserManager.ChangePasswordAsync(
                user, userChangePassword.CurrentPassword, userChangePassword.NewPassword);

            if (!changePasswordResult.Succeeded)
            {
                response.Success = false;
                response.Message = changePasswordResult.Errors.First().Description.ToString();
                return response;
            }

            response.Success = changePasswordResult.Succeeded;
            response.Message = "Password successfully changed";
            return response;
        }

        public Task<ServiceResponseDTO<UserEntity>> DeleteUser(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponseDTO<UserEntity>> GetUser(int id)
        {
            var response = new ServiceResponseDTO<UserEntity>();
            return response;
            //var userInDb = await 
        }
    }
}
