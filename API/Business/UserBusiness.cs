using App.Data;
using App.Model.User;
using App.Model;
using App.Utility;
using System.Security.Cryptography;
using System.Text;

namespace App.Business
{
    public class UserBusiness
    {
        private readonly UserData userData;
        private readonly Validation validator;

        public UserBusiness(UserData userData, Validation validator)
        {
            this.userData = userData;
            this.validator = validator;
        }

        public bool IsSuperUserBusiness(int userId)
        {
            if (userData.IsSuperUser(userId))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public BusinessResult<int> RegisterBusiness(UserRegisterModel registerModel)
        {
            BusinessResult<int> result = new BusinessResult<int>();

            string username = registerModel.Username;
            string email = registerModel.Email;
            string pw = registerModel.Password1;

            if (pw != registerModel.Password2)
            {
                result.SetError(400, "Passwords do not match.");

                return result;
            }

            string passwordError = validator.PasswordValidation(pw);

            if (passwordError != "")
            {
                result.SetError(400, passwordError);
            }

            byte[] hashedPW = MD5.HashData(Encoding.UTF8.GetBytes(pw));

            string usernameError = validator.UsernameValidation(username);

            if (usernameError != "")
            {
                result.SetError(400, usernameError);

                return result;
            }

            string emailError = validator.EmailValidation(email);

            if (emailError != "")
            {
                result.SetError(400, emailError);

                return result;
            }

            if (!userData.CheckUsernameAndEmailUniqueness(username, email))
            {
                result.SetError(400, "Username or email already taken.");

                return result;
            }

            if (string.IsNullOrEmpty(registerModel.Fullname))
            {
                registerModel.Fullname = username;
            }

            string Avatar;

            if (string.IsNullOrEmpty(registerModel.Avatar))
            {
                Avatar = "DefaultAvatar.png";
            }
            else
            {
                string avatarFilePath = @$"{Path.Combine(Directory.GetCurrentDirectory(), @"Static\Avatar\")}{registerModel.Username.ToLower()}.png";

                registerModel.Avatar = registerModel.Avatar.Replace("data:image/png;base64,", "");

                byte[] avatarBinaryData = Convert.FromBase64String(registerModel.Avatar);

                File.WriteAllBytes(avatarFilePath, avatarBinaryData);

                Avatar = $"{registerModel.Username.ToLower()}.png";
            }

            UserTable user = new UserTable()
            {
                Username = registerModel.Username,
                Email = registerModel.Email,
                Password = hashedPW,
                Fullname = registerModel.Fullname,
                Avatar = Avatar,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now,
                IsVerified = false,
                IsActive = true
            };

            int Id = userData.Register(user);

            if (Id == 0)
            {
                result.SetError(500, "Something went wrong.");
            }
            else
            {
                result.SetData(Id);
            }

            return result;
        }

        public BusinessResult<int> LoginBusiness(UserLoginModel loginModel)
        {
            BusinessResult<int> result = new BusinessResult<int>();

            byte[] hashedPW = MD5.HashData(Encoding.UTF8.GetBytes(loginModel.Password));

            int Id = userData.Login(loginModel.Username, hashedPW);

            if (Id == 0)
            {
                result.SetError(404, "Incorrect username or password.");
            }
            else
            {
                result.SetData(Id);
            }

            return result;
        }

        public BusinessResult<UserProfileModel> ProfileBusiness(int Id)
        {
            BusinessResult<UserProfileModel> result = new BusinessResult<UserProfileModel>();

            UserProfileModel profile = userData.GetUserProfileById(Id);

            if (profile.Username == "")
            {
                result.SetError(404, "Profile not found.");

                return result;
            }

            string avatarFilePath = @$"{Path.Combine(Directory.GetCurrentDirectory(), @"Static\Avatar\")}{profile.Avatar}";

            string avatarBase64Data = "data:image/png;base64," + Convert.ToBase64String(File.ReadAllBytes(avatarFilePath));

            profile.Avatar = avatarBase64Data;

            result.SetData(profile);

            return result;
        }

        public BusinessResult<VisitUserProfileModel> VisitProfileBusiness(string username)
        {
            BusinessResult<VisitUserProfileModel> result = new BusinessResult<VisitUserProfileModel>();

            VisitUserProfileModel profile = userData.VisitUserProfileByUsername(username);

            if (profile.Username == "")
            {
                result.SetError(404, "Profile not found.");

                return result;
            }

            string avatarFilePath = @$"{Path.Combine(Directory.GetCurrentDirectory(), @"Static\Avatar\")}{profile.Avatar}";

            string avatarBase64Data = "data:image/png;base64," + Convert.ToBase64String(File.ReadAllBytes(avatarFilePath));

            profile.Avatar = avatarBase64Data;

            result.SetData(profile);

            return result;
        }

        public BusinessResult<bool> EditProfileBusiness(UserUpdateModel updateModel)
        {
            BusinessResult<bool> result = new BusinessResult<bool>();

            UserTable user = userData.GetUser(updateModel.Id);

            if (user.Id == 0)
            {
                result.SetError(404, "User  not found.");
                return result;
            }

            string username = updateModel.Username;
            string email = updateModel.Email;

            if (string.IsNullOrEmpty(username) && string.IsNullOrEmpty(email))
            {
                updateModel.Username = user.Username;
                updateModel.Email = user.Email;
            }
            else
            {
                if (!userData.CheckUsernameAndEmailUniqueness(username, email))
                {
                    result.SetError(400, "Username or email already taken.");

                    return result;
                }

                if (!string.IsNullOrEmpty(username))
                {
                    string usernameError = validator.UsernameValidation(username);

                    if (usernameError != "")
                    {
                        result.SetError(400, usernameError);

                        return result;
                    }
                }
                else
                {
                    updateModel.Username = user.Username;
                }

                if (!string.IsNullOrEmpty(email))
                {
                    string emailError = validator.EmailValidation(email);

                    if (emailError != "")
                    {
                        result.SetError(400, emailError);

                        return result;
                    }
                }
                else
                {
                    updateModel.Email = user.Email;
                }
            }

            if (string.IsNullOrEmpty(updateModel.Fullname))
            {
                updateModel.Fullname = user.Fullname;
            }
            else if (updateModel.Fullname.Length > 50)
            {
                result.SetError(400, "Fullname must be less than 50 characters.");

                return result;
            }

            if (!string.IsNullOrEmpty(updateModel.Avatar))
            {
                try
                {
                    string oldAvatarFilePath = @$"{Path.Combine(Directory.GetCurrentDirectory(), @"Static\Avatar\")}{user.Username.ToLower()}.png";
                    string avatarFilePath = @$"{Path.Combine(Directory.GetCurrentDirectory(), @"Static\Avatar\")}{updateModel.Username.ToLower()}.png";
                    updateModel.Avatar = $"{updateModel.Username.ToLower()}.png";

                    updateModel.Avatar = updateModel.Avatar.Replace("data:image/png;base64,", "");

                    byte[] avatarBinaryData = Convert.FromBase64String(updateModel.Avatar);

                    File.WriteAllBytes(avatarFilePath, avatarBinaryData);

                    if (File.Exists(oldAvatarFilePath))
                    {
                        File.Delete(oldAvatarFilePath);
                    }
                }
                catch (Exception)
                {
                    result.SetError(500, "Womething went wrong.");

                    return result;
                }
            }
            else
            {
                updateModel.Avatar = user.Avatar;
            }

            UserTable updatedUser = new UserTable()
            {
                Id = user.Id,
                Username = updateModel.Username,
                Email = updateModel.Email,
                Password = user.Password,
                Fullname = updateModel.Fullname,
                Avatar = updateModel.Avatar,
                DateCreated = user.DateCreated,
                DateModified = DateTime.Now,
                IsVerified = user.IsVerified,
                IsActive = user.IsActive
            };

            if (!userData.EditUser(updatedUser))
            {
                result.SetError(500, "Womething went wrong.");
            }
            else
            {
                result.SetData(true);
            }

            return result;
        }

        public BusinessResult<bool> DeleteProfileBusiness(int Id)
        {
            BusinessResult<bool> result = new BusinessResult<bool>();

            UserTable user = userData.GetUser(Id);

            if (user.Id == 0)
            {
                result.SetError(404, "User  not found.");

                return result;
            }

            if (userData.DeleteUser(Id))
            {
                string avatarFilePath = @$"{Path.Combine(Directory.GetCurrentDirectory(), @"Static\Avatar\")}{user.Username.ToLower()}.png";

                if (File.Exists(avatarFilePath))
                {
                    File.Delete(avatarFilePath);
                }

                result.SetData(true);
            }
            else
            {
                result.SetError(500, "Something went wrong.");
            }

            return result;
        }
    }
}
