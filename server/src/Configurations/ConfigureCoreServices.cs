using CRUD.Data;
using CRUD.Data.Queries;
using CRUD.Models;
using FluentValidation.AspNetCore;

namespace CRUD.Configurations;

public static class ConfigureCoreServices
{
    public static IServiceCollection AddCoreServices(this IServiceCollection services)
    {
        services.AddFluentValidation(options =>
            options.RegisterValidatorsFromAssemblyContaining<AddBookRequest>()
        );

        services.AddSingleton<IRepository, Repository>();
        services.AddTransient<IBookQueries, BookQueries>();

        return services;
    }
}
