using System.ComponentModel.DataAnnotations;

namespace App.Model.Book
{
    public class CategoryTable
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public string Summary { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }
    }
}
