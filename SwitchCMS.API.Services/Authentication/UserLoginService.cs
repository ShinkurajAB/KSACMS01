using Microsoft.Extensions.Configuration;
using SwitchCMS.API.Services.Interface;
using SwitchCMS.APIServices.Authentication;
using SwitchCMS.Model;
using SwitchCMS.Model.Authentication;
using SwitchCMS.Utility;
using System.IdentityModel.Tokens.Jwt;

namespace SwitchCMS.API.Services.Authentication
{
    public interface IUserLoginService
    {
        public Task<AuthenticationResponseModel> Authenticate(string userName, string password, string browserDetails);
        UserAccount GetUserDetails(string jwtToken);
    }
    public class UserLoginService : IUserLoginService
    {

       //private JwtSecurityTokenHandler TokenHandler;
        private readonly IHashPasswordService hashPasswordService;
        private readonly IConfiguration _configuration;
        private readonly IOUSRService oUSRService;
        private readonly IJWTService JWTService;
        private readonly string Key;

        public UserLoginService( IHashPasswordService hashPasswordService, IConfiguration configuration, IOUSRService oUSRService, IJWTService jWTService)
        {
            
            this.hashPasswordService = hashPasswordService;
            _configuration = configuration;
            this.oUSRService = oUSRService;
            JWTService = jWTService;
            Key = _configuration.GetSection("JWT:Key").Value!;
            //tokenHandler = new JwtSecurityTokenHandler();
        }

        public async Task<AuthenticationResponseModel> Authenticate(string userName, string password, string browserDetails)
        {
            AuthenticationResponseModel responseModel = new AuthenticationResponseModel();
            OUSR loginModel = await oUSRService.GetUserByUserName(userName);
            if (loginModel == null)
            {
                responseModel.message = "Incorrect UserName";
                responseModel.Success = false;
                responseModel.ErrorID = 1;
                return responseModel;
            }
            bool verifypassword = hashPasswordService.Verify(password, loginModel.Password!);
            if (!verifypassword)
            {
                responseModel.message = "Incorrect Password";
                responseModel.Success = false;
                responseModel.ErrorID = 2;
                return responseModel;
            }

            // check User Inactive
            if(loginModel.Status==Status.InActive)
            {
                responseModel.message = "this User is InActive";
                responseModel.Success = false;
                responseModel.ErrorID = 3;
                return responseModel;
            }


            // Check User Company Status
            if(loginModel.Company.Status==CompanyStatus.InActive)
            {
                responseModel.message = "Your Company is InActive Please Contact Admin";
                responseModel.Success = false;
                responseModel.ErrorID = 4;
                return responseModel;
            }


            responseModel.Id = loginModel.ID.ToString();
            responseModel.Role = loginModel.Role;
            responseModel.UserName = loginModel.UserName;

            UserAccount user = new UserAccount() { User = loginModel, Claims = new List<UserClaimModel>() };
            responseModel.JwtToken = JWTService.GenerateAccessToken(user);
            responseModel.RefreshToken = JWTService.GenerateRefreshToken(user.User.ID, user.User.UserName, user.User.Password, browserDetails);
            responseModel.Success = true;
            return responseModel;
        }


        public UserAccount GetUserDetails(string jwtToken)
        {
            UserAccount user = JWTService.GetUserDetails(jwtToken);
            return user;
        }
    }
}
