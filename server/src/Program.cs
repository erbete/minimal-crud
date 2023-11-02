using System.Data;
using CRUD.Configurations;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCoreServices();
builder.Services.AddTransient<IDbConnection>(_ =>
    new NpgsqlConnection(builder.Configuration.GetConnectionString("BooksDb"))
);

var app = builder.Build();
app.UseCoreServices();
app.Run();
