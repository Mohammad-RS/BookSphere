using System.ComponentModel.DataAnnotations;

namespace App.Model.Book
{
    public class BookTable
    {
        public int Id { get; set; }

        [Required]
        public int AuthorId { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        public string Summary { get; set; }

        [StringLength(20)]
        public string ISBN { get; set; }

        public int AvailableCopies { get; set; }

        public string CoverImage { get; set; }

        public DateTime DateCreated { get; set; }
        
        public DateTime DateModified { get; set; }
    }
}
