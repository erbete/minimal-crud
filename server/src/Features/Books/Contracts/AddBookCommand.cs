using MediatR;

using CRUD.Data;
using CRUD.Features.Books.Contracts.DTO;

namespace CRUD.Features.Books.Contracts;

public record AddBookCommand(AddBookRequest NewBook) : IRequest<BookResponse>;

public class AddBookHandler : IRequestHandler<AddBookCommand, BookResponse?>
{
    private readonly IRepository _repository;

    public AddBookHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<BookResponse?> Handle(AddBookCommand request, CancellationToken cancellationToken)
    {
        Guid isbn = Guid.Parse(request.NewBook.ISBN);
        bool isRemoved = await _repository.IsBookRecordRemovedAsync(isbn);
        if (isRemoved) return null;

        var newBook = request.NewBook;
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