using App.Data;
using App.Model;
using App.Utility;
using App.Model.Book;
using System.Collections.Generic;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace App.Business
{
    public class BookBusiness
    {
        private readonly BookData bookData;
        private readonly Validation validator;

        public BookBusiness(BookData bookData, Validation validator)
        {
            this.bookData = bookData;
            this.validator = validator;
        }

        public BusinessResult<int> AddAuthorBusiness(AuthorTable authorAddModel)
        {
            BusinessResult<int> result = new BusinessResult<int>();

            if (bookData.DoesInstanceExist<AuthorTable>(authorAddModel.Name))
            {
                result.SetError(400, "Author already exists.");

                return result;
            }
            else
            {
                authorAddModel.DateCreated = authorAddModel.DateModified = DateTime.Now;

                result.SetData(bookData.Add<AuthorTable>(authorAddModel));

                return result;
            }
        }

        public BusinessResult<int> AddCategoryBusiness(CategoryTable categoryAddModel)
        {
            BusinessResult<int> result = new BusinessResult<int>();

            if (bookData.DoesInstanceExist<CategoryTable>(categoryAddModel.Name))
            {
                result.SetError(400, "Category already exists.");

                return result;
            }
            else
            {
                categoryAddModel.DateCreated = categoryAddModel.DateModified = DateTime.Now;

                result.SetData(bookData.Add<CategoryTable>(categoryAddModel));

                return result;
            }
        }

        public BusinessResult<int> AddBookBusiness(BookAddModel bookAddModel)
        {
            BusinessResult<int> result = new BusinessResult<int>();

            if (bookData.DoesBookExist(bookAddModel.Title))
            {
                result.SetError(400, "Book is already registered.");
                return result;
            }

            AuthorTable author = bookData.GetByName<AuthorTable>(bookAddModel.Author);

            if (author == null)
            {
                author.Id = bookData.Add<AuthorTable>(new AuthorTable() { Name = bookAddModel.Author });
            }

            CategoryTable category = bookData.GetByName<CategoryTable>(bookAddModel.Category);

            if (category == null)
            {
                category.Id = bookData.Add<CategoryTable>(new CategoryTable() { Name = bookAddModel.Category });
            }

            string img;

            if (string.IsNullOrEmpty(bookAddModel.CoverImage))
            {
                img = "DefaultBook.png";
            }
            else
            {
                string imgFilePath = @$"{Path.Combine(Directory.GetCurrentDirectory(), @"Static\Cover\")}{bookAddModel.Title.ToLower()}.png";

                bookAddModel.CoverImage = bookAddModel.CoverImage.Replace("data:image/png;base64,", "");

                byte[] imgBinaryData = Convert.FromBase64String(bookAddModel.CoverImage);

                File.WriteAllBytes(imgFilePath, imgBinaryData);

                img = $"{bookAddModel.Title.ToLower()}.png";
            }

            BookTable book = new BookTable()
            {
                AuthorId = author.Id,
                CategoryId = category.Id,
                Title = bookAddModel.Title,
                Summary = bookAddModel.Summary,
                ISBN = bookAddModel.ISBN,
                CoverImage = img,
                AvailableCopies = bookAddModel.AvailableCopies,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now
            };

            int Id = bookData.Add<BookTable>(book);

            if (Id == 0)
            {
                result.SetError(500, "Something went wrong.");
            }
            else
            {
                result.SetData(Id);
            }

            return result;
        }

        public BusinessResult<int> AddReviewBusiness(ReviewTable reviewAddModel)
        {
            BusinessResult<int> result = new BusinessResult<int>();

            int Id = bookData.Add<ReviewTable>(reviewAddModel);

            if (Id == 0)
            {
                result.SetError(500, "Something went wrong.");
            }
            else
            {
                result.SetData(Id);
            }

            return result;
        }

        public BusinessResult<IEnumerable<T>> GetAllBusiness<T>()
        {
            BusinessResult<IEnumerable<T>> result = new BusinessResult<IEnumerable<T>>();

            IEnumerable<T> data = bookData.GetAll<T>();

            if (data == null)
            {
                result.SetError(500, "Something went wrong.");
            }
            else
            {
                result.SetData(data);
            }

            return result;
        }

        public BusinessResult<IEnumerable<GetAllBooksModel>> GetAllBooksBusiness<T>()
        {
            BusinessResult<IEnumerable<GetAllBooksModel>> result = new BusinessResult<IEnumerable<GetAllBooksModel>>();

            IEnumerable<GetAllBooksModel> books = bookData.GetAllBooks();

            if (books == null)
            {
                result.SetError(500, "Something went wrong.");
            }
            else
            {
                foreach (var book in books)
                {
                    string imgFilePath = @$"{Path.Combine(Directory.GetCurrentDirectory(), @"Static\Cover\")}{book.CoverImage}";

                    string imgBase64Data = "data:image/png;base64," + Convert.ToBase64String(File.ReadAllBytes(imgFilePath));

                    book.CoverImage = imgBase64Data;
                }

                result.SetData(books);
            }

            return result;
        }

        public BusinessResult<GetBookDetailsModel> GetBookDetailsBusiness(int bookId)
        {
            BusinessResult<GetBookDetailsModel> result = new BusinessResult<GetBookDetailsModel>();

            GetBookDetailsModel book = bookData.GetBookDetails(bookId);

            if (book == null)
            {
                result.SetError(500, "Something went wrong.");
            }
            else
            {
                string imgFilePath = @$"{Path.Combine(Directory.GetCurrentDirectory(), @"Static\Cover\")}{book.CoverImage}";

                string imgBase64Data = "data:image/png;base64," + Convert.ToBase64String(File.ReadAllBytes(imgFilePath));

                book.CoverImage = imgBase64Data;

                result.SetData(book);
            }

            return result;
        }
    }
}