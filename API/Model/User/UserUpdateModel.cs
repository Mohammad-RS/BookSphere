using System.ComponentModel.DataAnnotations;

namespace App.Model.User
{
    public class UserUpdateModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(50)]
        public string Fullname { get; set; }

        public string Avatar { get; set; }
    }
}
