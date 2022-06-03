using BISA.Server.Services.AuthService;
using BISA.Shared.DTO;
using FakeItEasy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BISA.Server.Tests
{
    public class AuthControllerTests
    {
        [Fact]
        public void ReturnJWTWhenLoginAsAdmin()
        {
            // arrange
            UserLoginDTO userLoginDTO = new UserLoginDTO
            {
                Username = "admin",
                Password = "admin"
            };
            ServiceResponseDTO<string> serviceResponse = new();
            var service = A.Fake<IAuthService>();
            //A.CallTo(() => service.Login(userLoginDTO)).Returns(serviceResponse);



            // act

            // assert
        }

    }
}
