using FluentValidation;
using MediatR;
using Carter;

using CRUD.Features.Books.Contracts;
using CRUD.Features.Books.Contracts.DTO;

namespace CRUD.Features.Books;

public class Routes : CarterModule
{
    public Routes() : base("/api/books")
    {
        // this.RequireAuthorization();
    }

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/all", ListAllHandlerAsync)
            .Produces(StatusCodes.Status200OK);

        app.MapGet("/{isbn}", GetHandlerAsync)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status200OK);

        app.MapPost("/", AddHandlerAsync)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status201Created);

        app.MapDelete("/{isbn}", RemoveHandlerAsync)
            .Produces(StatusCodes.Status204NoContent);
    }

    private async Task<IResult> ListAllHandlerAsync(IMediator mediator)
    {
        var books = await mediator.Send(new ListAllBooksQuery());
        return Results.Ok(books);
    }

    private async Task<IResult> GetHandlerAsync(IMediator mediator, Guid isbn)
    {
        var book = await mediator.Send(new GetBookQuery(isbn));
        if (book is null) return Results.NotFound();
        return Results.Ok(book);
    }

    private async Task<IResult> AddHandlerAsync(
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

    private async Task<IResult> RemoveHandlerAsync(IMediator mediator, Guid isbn)
    {
        await mediator.Send(new RemoveBookCommand(isbn));
        return Results.NoContent();
    }
}