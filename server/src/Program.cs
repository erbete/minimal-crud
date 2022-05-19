using System.Data;
using CRUD.Endpoints;
using CRUD.Configurations;
using CRUD.Extensions;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCoreServices();
builder.Services.AddTransient<IDbConnection>(_ =>
    new NpgsqlConnection(builder.Configuration.GetConnectionString("BooksDb"))
);

var app = builder.Build();
app.UseMiddleware<ExceptionMiddleware>();
app.UseCoreServices();
app.UseEndpoints();
app.Run();
