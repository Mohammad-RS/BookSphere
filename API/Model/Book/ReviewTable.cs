using System.ComponentModel.DataAnnotations;

namespace App.Model.Book
{
    public class ReviewTable
    {
        public int Id { get; set; }

        [Required]
        public int BookId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Range(1, 10)]
        public int Rating { get; set; }

        public string Comment { get; set; }
    }
}
