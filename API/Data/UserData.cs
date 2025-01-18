using Microsoft.Data.SqlClient;
using App.Utility;
using App.Model.User;
using Dapper;

namespace App.Data
{
    public class UserData
    {
        private readonly SqlConnection conn;
        private readonly Crud crud;

        public UserData(SqlConnection conn, Crud crud)
        {
            this.conn = conn;
            this.crud = crud;
        }

        public bool IsSuperUser(int userId)
        {
            try
            {
                string query = "SELECT COUNT(*) FROM dbo.Staff WHERE Id = @Id";

                int count = conn.ExecuteScalar<int>(query, new { Id = userId });

                return count == 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public int Register(UserTable user)
        {
            try
            {
                return crud.Insert(user);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int Login(string username, byte[] password)
        {
            string query = "SELECT Id FROM dbo.[User] WHERE Username = @Username AND Password = @Password";

            try
            {
                return conn.ExecuteScalar<int>(query, new { Username = username, Password = password });
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public bool EditUser(UserTable user)
        {
            try
            {
                return crud.UpdateById(user);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteUser(int Id)
        {
            try
            {
                return crud.DeleteById<UserTable>(Id);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public UserTable GetUser(int userId)
        {
            try
            {
                return crud.GetById<UserTable>(userId);
            }
            catch (Exception)
            {
                return new UserTable();
            }
        }

        public UserProfileModel GetUserProfileById(int userId)
        {
            try
            {
                UserTable user = crud.GetById<UserTable>(userId);

                UserProfileModel profile = new UserProfileModel()
                {
                    Username = user.Username,
                    Email = user.Email,
                    Fullname = user.Fullname,
                    Avatar = user.Avatar,
                    DateCreated = user.DateCreated,
                    IsVerified = user.IsVerified,
                    IsActive = user.IsActive
                };

                return profile;
            }
            catch (Exception)
            {
                return new UserProfileModel();
            }
        }

        public VisitUserProfileModel VisitUserProfileByUsername(string username)
        {
            string query = "SELECT Id FROM dbo.[User] WHERE Username = @Username";

            try
            {
                int Id = conn.ExecuteScalar<int>(query, new { Username = username });

                UserTable user = crud.GetById<UserTable>(Id);

                VisitUserProfileModel profile = new VisitUserProfileModel()
                {
                    Username = user.Username,
                    Fullname = user.Fullname,
                    Avatar = user.Avatar,
                    DateCreated = user.DateCreated,
                    IsActive = user.IsActive
                };

                return profile;
            }
            catch (Exception)
            {
                return new VisitUserProfileModel();
            }
        }

        public bool CheckUsernameAndEmailUniqueness(string username, string email)
        {
            try
            {
                string query = "SELECT COUNT(*) FROM dbo.[User] WHERE Username = @Username OR Email = @Email";

                int count = conn.ExecuteScalar<int>(query, new { Username = username, Email = email });

                return count == 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
