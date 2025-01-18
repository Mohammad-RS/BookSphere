using System.ComponentModel.DataAnnotations;

namespace App.Model.Book
{
    public class GetBookDetailsModel
    {
        public int Id { get; set; }

        public string Author { get; set; }

        public string Category { get; set; }

        public string Title { get; set; }

        public string Summary { get; set; }

        public string ISBN { get; set; }

        public int AvailableCopies { get; set; }

        public string CoverImage { get; set; }

        public DateTime DateCreated { get; set; }
    }
}