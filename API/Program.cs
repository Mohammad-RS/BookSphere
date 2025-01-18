using Microsoft.Data.SqlClient;
using App.Utility;
using App.Data;
using App.Business;

namespace App
{
    public class Program
    {
        public const string connectionString = "Data Source=.;Database=BookSphere;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False";

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddTransient<SqlConnection>(x => new SqlConnection(connectionString));
            builder.Services.AddTransient<Crud>();
            builder.Services.AddTransient<Validation>();

            builder.Services.AddTransient<UserBusiness>();
            builder.Services.AddTransient<BookBusiness>();

            builder.Services.AddTransient<UserData>();
            builder.Services.AddTransient<BookData>();

            builder.Services.AddAuthorization();
            builder.Services.AddAuthentication().AddJwtBearer(x =>
            {
                x.TokenValidationParameters = Token.Params;
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseCors(x =>
            {
                x.AllowAnyHeader();
                x.AllowAnyMethod();
                x.AllowAnyOrigin();
            });

            app.UseHttpsRedirection();
            
            app.UseRouting();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
