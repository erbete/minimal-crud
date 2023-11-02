using MediatR;

using CRUD.Data;
using CRUD.Features.Books.Contracts.DTO;

namespace CRUD.Features.Books.Contracts;

public record GetBookQuery(Guid ISBN) : IRequest<BookResponse>;

public class GetBookQueryHandler : IRequestHandler<GetBookQuery, BookResponse?>
{
    private readonly IRepository _repository;

    public GetBookQueryHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<BookResponse?> Handle(GetBookQuery request, CancellationToken cancellationToken)
    {
        var book = await _repository.GetBookAsync(request.ISBN);
        if (book is null) return null;
        return new BookResponse(book.ISBN, book.Title, book.Author, book.Pages);
    }
}