using System.ComponentModel.DataAnnotations;

namespace App.Model.User
{
    public class UserLoginModel
    {
        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string Username { get; set; }

        [Required]
        [StringLength(16, MinimumLength = 8)]
        public string Password { get; set; }
    }
}
