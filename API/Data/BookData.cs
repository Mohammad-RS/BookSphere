using App.Model.Book;
using App.Utility;
using Dapper;
using Microsoft.Data.SqlClient;
using System.ComponentModel.DataAnnotations;

namespace App.Data
{
    public class BookData
    {
        private readonly SqlConnection conn;
        private readonly Crud crud;

        public BookData(SqlConnection conn, Crud crud)
        {
            this.conn = conn;
            this.crud = crud;
        }

        public int Add<T>(T instance)
        {
            try
            {
                return crud.Insert(instance);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public bool DoesInstanceExist<T>(int instanceId)
        {
            try
            {
                if (crud.GetById<T>(instanceId) == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DoesInstanceExist<T>(string name)
        {
            Validation validator = new Validation();

            Type tableType = typeof(T);

            string table = tableType.Name.Replace("Table", "");

            if (!validator.IsTableNameValid(table))
            {
                throw new Exception("Invalid table name");
            }

            string query = $"SELECT Count(*) FROM [dbo].[{table}] WHERE Name = @Name";

            int count = this.conn.Execute(query, new { Name = name });

            if (count != -1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DoesBookExist(string title)
        {
            string query = $"SELECT Count(*) FROM [dbo].[Book] WHERE Title = @Title";

            int count = this.conn.Execute(query, new { Title = title });

            if (count != -1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public T GetByName<T>(string name)
        {
            Validation validator = new Validation();

            Type tableType = typeof(T);

            string table = tableType.Name.Replace("Table", "");

            if (!validator.IsTableNameValid(table))
            {
                throw new Exception("Invalid table name");
            }

            string query = $"SELECT * FROM [dbo].[{table}] WHERE Name = @Name";

            return this.conn.QuerySingleOrDefault<T>(query, new { Name = name });

        }

        public IEnumerable<T> GetAll<T>()
        {
            return this.crud.Select<T>();
        }

        public IEnumerable<GetAllBooksModel> GetAllBooks()
        {
            string query = $@"SELECT B.Id, A.[Name] AS Author, C.[Name] AS Category, B.Title, B.AvailableCopies, B.CoverImage FROM [dbo].[Book] B INNER JOIN dbo.[Author] A ON B.AuthorId = A.Id INNER JOIN dbo.[Category] C ON B.CategoryId = C.Id";

            return this.conn.Query<GetAllBooksModel>(query);
        }

        public GetBookDetailsModel GetBookDetails(int bookId)
        {
            string query = $@"SELECT B.Id, A.[Name] AS Author, C.[Name] AS Category, B.Title, B.Summary, B.ISBN, B.AvailableCopies, B.CoverImage, B.DateCreated FROM [dbo].[Book] B INNER JOIN dbo.[Author] A ON B.AuthorId = A.Id INNER JOIN dbo.[Category] C ON B.CategoryId = C.Id WHERE B.Id = @BookId";

            return this.conn.QuerySingle<GetBookDetailsModel>(query, new { BookId = bookId });
        }
    }
}