using Dapper;
using Microsoft.Data.SqlClient;
using System.Reflection;

namespace App.Utility
{
    public class Crud
    {
        private readonly SqlConnection conn;
        private readonly Validation validator;

        public Crud(SqlConnection conn, Validation validator)
        {
            this.conn = conn;
            this.validator = validator;
        }

        public int Insert<T>(T CreateModel)
        {
            Type tableType = typeof(T);

            string table = tableType.Name.Replace("Table", "");

            if (CreateModel == null)
            {
                throw new Exception($"{table} create model cannot be null.");
            }

            if (!validator.IsTableNameValid(table))
            {
                throw new Exception("Invalid table name");
            }

            PropertyInfo[] properties = tableType.GetProperties();

            List<string> fields = new();
            List<string> parameters = new();

            string output = "";

            foreach (PropertyInfo property in properties)
            {
                if (property.Name == "Id")
                {
                    output = "OUTPUT inserted.ID";
                    continue;
                }

                fields.Add($"[{property.Name}]");
                parameters.Add($"@{property.Name}");
            }

            string csvFields = string.Join(", ", fields);
            string csvParams = string.Join(", ", parameters);

            string query = $"INSERT INTO [dbo].[{table}] ({csvFields}) {output} VALUES ({csvParams})";

            return this.conn.ExecuteScalar<int>(query, CreateModel);
        }

        public IEnumerable<T> Select<T>()
        {
            Type tableType = typeof(T);

            string table = tableType.Name.Replace("Table", "");

            if (!validator.IsTableNameValid(table))
            {
                throw new Exception("Invalid table name");
            }

            string query = $"SELECT * FROM [dbo].[{table}]";

            return this.conn.Query<T>(query);
        }

        public T GetById<T>(int instanceId)
        {
            Type tableType = typeof(T);

            string table = tableType.Name.Replace("Table", "");

            if (!validator.IsTableNameValid(table))
            {
                throw new Exception("Invalid table name");
            }

            string query = $"SELECT * FROM [dbo].[{table}] WHERE Id = @Id";

            return this.conn.QuerySingle<T>(query, new { Id = instanceId });
        }

        public bool UpdateById<T>(T UpdateModel)
        {
            Type tableType = typeof(T);

            string table = tableType.Name.Replace("Table", "");

            if (!validator.IsTableNameValid(table))
            {
                throw new Exception("Invalid table name");
            }

            PropertyInfo[] properties = tableType.GetProperties();

            List<string> equals = new();

            foreach (PropertyInfo property in properties)
            {
                if (property.Name == "Id")
                {
                    continue;
                }

                equals.Add($"[{property.Name}] = @{property.Name}");
            }

            string csvEquals = string.Join(", ", equals);

            string query = $"UPDATE [dbo].[{table}] SET {csvEquals} WHERE Id = @Id";

            int rowsAffected = this.conn.Execute(query, UpdateModel);

            return rowsAffected > 0;
        }

        public bool DeleteById<T>(int instanceId)
        {
            Type tableType = typeof(T);

            string table = tableType.Name.Replace("Table", "");

            if (!validator.IsTableNameValid(table))
            {
                throw new Exception("Invalid table name");
            }

            string query = $"DELETE FROM [dbo].[{table}] WHERE Id = @Id";

            int rowsAffected = this.conn.Execute(query, new { Id = instanceId });

            return rowsAffected > 0;
        }
    }
}
