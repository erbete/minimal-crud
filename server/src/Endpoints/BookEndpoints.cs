using CRUD.Data;
using CRUD.Models;
using FluentValidation;

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

    private static async Task<IResult> ListAllHandlerAsync(IRepository repository)
    {
        var books = await repository.ListBooksAsync();
        var response = books.Select(b => new BookResponse(b.ISBN, b.Title, b.Author, b.Pages));
        return Results.Ok(response);
    }

    private static async Task<IResult> GetHandlerAsync(IRepository repository, Guid isbn)
    {
        var book = await repository.GetBookAsync(isbn);
        if (book is null) return Results.NotFound();
        return Results.Ok(new BookResponse(book.ISBN, book.Title, book.Author, book.Pages));
    }

    private static async Task<IResult> AddHandlerAsync(
        IRepository repository,
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

        bool isRemoved = await repository.IsBookRecordRemovedAsync(Guid.Parse(newBook.ISBN));
        if (isRemoved) return Results.BadRequest();

        await repository.AddBookAsync(new BookData()
        {
            ISBN = Guid.Parse(newBook.ISBN),
            Title = newBook.Title.Trim(),
            Author = newBook.Author.Trim(),
            Pages = newBook.Pages
        });

        return Results.Created($"/book/{newBook.ISBN}", newBook);
    }

    private static async Task<IResult> RemoveHandlerAsync(IRepository repository, Guid isbn)
    {
        await repository.RemoveBookAsync(isbn);
        return Results.NoContent();
    }
}