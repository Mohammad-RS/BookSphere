namespace App.Model.Book
{
    public class GetAllBooksModel
    {
        public int Id { get; set; }

        public string Author { get; set; }

        public string Category { get; set; }

        public string Title { get; set; }

        public int AvailableCopies { get; set; }

        public string CoverImage { get; set; }
    }
}
