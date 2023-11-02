using FluentValidation;
using Carter;
using CRUD.Data;
using CRUD.Data.Queries;
using CRUD.Models;
using CRUD.Extensions;

namespace CRUD.Configurations;

public static class ConfigureCoreServices
{
    public static IServiceCollection AddCoreServices(this IServiceCollection services)
    {
        services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(Program).Assembly));
        services.AddValidatorsFromAssemblyContaining<AddBookRequest>();
        services.AddSingleton<IRepository, Repository>();
        services.AddTransient<IBookQueries, BookQueries>();
        services.AddSwaggerGen();
        services.AddCarter();
        services.AddEndpointsApiExplorer();

        return services;
    }

    public static void UseCoreServices(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CRUD v1"));
        app.UseMiddleware<ExceptionMiddleware>();
        app.MapCarter();
    }
}