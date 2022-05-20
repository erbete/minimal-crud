using System.Reflection;
using CRUD.Data;
using CRUD.Data.Queries;
using CRUD.Models;
using FluentValidation.AspNetCore;
using MediatR;

namespace CRUD.Configurations;

public static class ConfigureCoreServices
{
    public static IServiceCollection AddCoreServices(this IServiceCollection services)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());

        services.AddFluentValidation(options =>
            options.RegisterValidatorsFromAssemblyContaining<AddBookRequest>()
        );

        services.AddSingleton<IRepository, Repository>();
        services.AddTransient<IBookQueries, BookQueries>();

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        return services;
    }

    public static void UseCoreServices(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CRUD v1"));
    }
}