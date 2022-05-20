using CRUD.Endpoints.Events.Commands;
using CRUD.Endpoints.Events.Queries;
using CRUD.Models;
using FluentValidation;
using MediatR;

namespace CRUD.Endpoints;

internal static class BookEndpoints
{
    public static void Configure(WebApplication app)
    {
        app.MapGet("/book/all", ListAllHandlerAsync)
            .Produces(StatusCodes.Status200OK);

        app.MapGet("/book/{isbn}", GetHandlerAsync)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status200OK);

        app.MapPost("/book", AddHandlerAsync)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status201Created);

        app.MapDelete("/book/{isbn}", RemoveHandlerAsync)
            .Produces(StatusCodes.Status204NoContent);
    }

    private static async Task<IResult> ListAllHandlerAsync(IMediator mediator)
    {
        var books = await mediator.Send(new ListAllBooksQuery());
        return Results.Ok(books);
    }

    private static async Task<IResult> GetHandlerAsync(IMediator mediator, Guid isbn)
    {
        var book = await mediator.Send(new GetBookQuery(isbn));
        if (book is null) return Results.NotFound();
        return Results.Ok(book);
    }

    private static async Task<IResult> AddHandlerAsync(
        IMediator mediator,
        IValidator<AddBookRequest> validator,
        AddBookRequest newBook
    )
    {
        var validationResult = validator.Validate(newBook);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(e => e.ErrorMessage);
            return Results.BadRequest(errors);
        }

        var book = await mediator.Send(new AddBookCommand(newBook));
        if (book is null) return Results.BadRequest();
        return Results.Created($"/book/{book.ISBN}", book);
    }

    private static async Task<IResult> RemoveHandlerAsync(IMediator mediator, Guid isbn)
    {
        await mediator.Send(new RemoveBookCommand(isbn));
        return Results.NoContent();
    }
}