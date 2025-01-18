using App.Business;
using App.Model.User;
using App.Model;
using App.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using App.Model.Book;
using System.Net;

namespace App.Controller
{
    //[Authorize]
    [ApiController]
    [Route("book")]
    public class BookController : ControllerBase
    {
        private BookBusiness bookBusiness;
        private UserBusiness UserBusiness;
        /*
         Add The commented lines to enable the authorization for users.
         */

        public BookController(BookBusiness bookBusiness, UserBusiness userBusiness)
        {
            this.bookBusiness = bookBusiness;
            this.UserBusiness = userBusiness;
        }

        [HttpPost("add-author")]
        public BusinessResult<int> AddAuthor(AuthorTable authorTable)
        {
            //int Id = int.Parse(base.User.Identity.Name);

            //if (UserBusiness.IsSuperUserBusiness(Id))
            //{
            //    return bookBusiness.AddAuthorBusiness(authorTable);
            //}
            //else
            //{
            //    return new BusinessResult<int>
            //    {
            //        ErrorCode = 403,
            //        ErrorMessage = "You are not authorized to perform this action."
            //    };
            //}

            return bookBusiness.AddAuthorBusiness(authorTable);
        }

        [HttpPost("add-category")]
        public BusinessResult<int> AddCategory(CategoryTable categoryTable)
        {
            //int Id = int.Parse(base.User.Identity.Name);

            //if (UserBusiness.IsSuperUserBusiness(Id))
            //{
            //    return bookBusiness.AddCategoryBusiness(categoryTable);
            //}
            //else
            //{
            //    return new BusinessResult<int>
            //    {
            //        ErrorCode = 403,
            //        ErrorMessage = "You are not authorized to perform this action."
            //    };
            //}

            return bookBusiness.AddCategoryBusiness(categoryTable);
        }

        [HttpPost("add-book")]
        public BusinessResult<int> AddBook(BookAddModel bookAddModel)
        {
            //int Id = int.Parse(base.User.Identity.Name);

            //if (UserBusiness.IsSuperUserBusiness(Id))
            //{
            //    return bookBusiness.AddBookBusiness(bookAddModel);
            //}
            //else
            //{
            //    return new BusinessResult<int>
            //    {
            //        ErrorCode = 403,
            //        ErrorMessage = "You are not authorized to perform this action."
            //    };
            //}

            return bookBusiness.AddBookBusiness(bookAddModel);
        }

        [HttpPost("add-review")]
        public BusinessResult<int> AddReview(ReviewTable reviewTable)
        {
            return bookBusiness.AddReviewBusiness(reviewTable);
        }

        [HttpGet("authors")]
        public BusinessResult<IEnumerable<AuthorTable>> GetAllAuthors()
        {
            return bookBusiness.GetAllBusiness<AuthorTable>();
        }

        [HttpGet("categories")]
        public BusinessResult<IEnumerable<CategoryTable>> GetAllCategories()
        {
            return bookBusiness.GetAllBusiness<CategoryTable>();
        }

        [HttpGet("books")]
        public BusinessResult<IEnumerable<GetAllBooksModel>> GetAllBooks()
        {
            return bookBusiness.GetAllBooksBusiness<GetAllBooksModel>();
        }

        [HttpGet("books/{bookId}")]
        public BusinessResult<GetBookDetailsModel> GetBookDetails(int bookId)
        {
            return bookBusiness.GetBookDetailsBusiness(bookId);
        }
    }
}

