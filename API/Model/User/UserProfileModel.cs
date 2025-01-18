namespace App.Model.User
{
    public class UserProfileModel
    {
        public string Username { get; set; }

        public string Email { get; set; }

        public string Fullname { get; set; }

        public string Avatar { get; set; }

        public DateTime DateCreated { get; set; }

        public bool IsVerified { get; set; }

        public bool IsActive { get; set; }
    }
}
