using System.Data;
using CRUD.Api;
using CRUD.Configurations;
using CRUD.Extensions;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCoreServices();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IDbConnection>(_ =>
    new NpgsqlConnection(builder.Configuration.GetConnectionString("BooksDb"))
);

var app = builder.Build();
app.UseMiddleware<ExceptionMiddleware>();
app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CRUD v1"));
app.ConfigureApi();
app.Run();
