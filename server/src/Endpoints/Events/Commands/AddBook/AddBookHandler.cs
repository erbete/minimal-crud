using CRUD.Data;
using CRUD.Endpoints.Events.Commands;
using CRUD.Models;
using MediatR;

namespace CRUD.Endpoints.Events.Command;

public class AddBookHandler : IRequestHandler<AddBookCommand, BookResponse?>
{
    private readonly IRepository _repository;

    public AddBookHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<BookResponse?> Handle(AddBookCommand request, CancellationToken cancellationToken)
    {
        Guid isbn = Guid.Parse(request.newBook.ISBN);
        bool isRemoved = await _repository.IsBookRecordRemovedAsync(isbn);
        if (isRemoved) return null;

        var newBook = request.newBook;
        await _repository.AddBookAsync(new BookData()
        {
            ISBN = isbn,
            Title = newBook.Title.Trim(),
            Author = newBook.Author.Trim(),
            Pages = newBook.Pages
        });

        return new BookResponse(
            isbn,
            newBook.Title,
            newBook.Author,
            newBook.Pages
        );
    }
}