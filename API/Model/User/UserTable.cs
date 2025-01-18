using System.ComponentModel.DataAnnotations;

namespace App.Model.User
{
    public class UserTable
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string Username { get; set; }

        [Required]
        public byte[] Password { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(50)]
        public string Fullname { get; set; }
        
        [StringLength(100)]
        public string Avatar { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        public bool IsVerified { get; set; }

        public bool IsActive { get; set; }
    }
}
