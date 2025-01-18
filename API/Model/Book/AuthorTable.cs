using System.ComponentModel.DataAnnotations;

namespace App.Model.Book
{
    public class AuthorTable
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)] 
        public string Name { get; set; }

        public string Bio { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }
    }
}