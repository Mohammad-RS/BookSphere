using App.Business;
using App.Model.User;
using App.Model;
using App.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.Controller
{
    [ApiController]
    [Route("user")]
    public class UserController : ControllerBase
    {
        private UserBusiness userBusiness;

        public UserController(UserBusiness userBusiness)
        {
            this.userBusiness = userBusiness;
        }

        [HttpPost("register")]
        public BusinessResult<int> Register(UserRegisterModel registerModel)
        {
            return userBusiness.RegisterBusiness(registerModel);
        }

        [HttpPost("login")]
        public BusinessResult<string> Login(UserLoginModel loginModel)
        {
            BusinessResult<int> loginResult = userBusiness.LoginBusiness(loginModel);
            BusinessResult<string> result = new BusinessResult<string>();

            if (loginResult.Success)
            {
                int Id = loginResult.Data;
                string token = Token.Generate(Id);

                result.SetData(token);
            }
            else
            {
                result.SetError(loginResult.ErrorCode, loginResult.ErrorMessage);
            }

            return result;
        }

        [Authorize]
        [HttpGet("profile")]
        public BusinessResult<UserProfileModel> Profile()
        {
            int Id = int.Parse(base.User.Identity.Name);

            return userBusiness.ProfileBusiness(Id);
        }

        [HttpGet("{username}/profile")]
        public BusinessResult<VisitUserProfileModel> VisitProfile(string username)
        {
            return userBusiness.VisitProfileBusiness($"{username}");
        }

        [Authorize]
        [HttpPut("profile/edit")]
        public BusinessResult<bool> EditProfile(UserUpdateModel updateModel)
        {
            int Id = int.Parse(base.User.Identity.Name);

            updateModel.Id = Id;

            return userBusiness.EditProfileBusiness(updateModel);
        }

        [Authorize]
        [HttpDelete("profile/delete")]
        public BusinessResult<bool> DeleteProfile()
        {
            int Id = int.Parse(base.User.Identity.Name);

            return userBusiness.DeleteProfileBusiness(Id);
        }
    }
}
