using System.ComponentModel.DataAnnotations;

namespace App.Model.Book
{
    public class BookAddModel
    {
        [Required]
        public string Author { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        public string Summary { get; set; }

        [StringLength(20)]
        public string ISBN { get; set; }

        [Required]
        public int AvailableCopies { get; set; }

        public string CoverImage { get; set; }
    }
}
