using MediatR;

using CRUD.Data;
using CRUD.Features.Books.Contracts.DTO;

namespace CRUD.Features.Books.Contracts;

public record ListAllBooksQuery() : IRequest<IReadOnlyList<BookResponse>>;

public class ListAllBooksQueryHandler : IRequestHandler<ListAllBooksQuery, IReadOnlyList<BookResponse>>
{
    private readonly IRepository _repository;

    public ListAllBooksQueryHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<BookResponse>> Handle(ListAllBooksQuery request, CancellationToken cancellationToken)
    {
        var books = await _repository.ListBooksAsync();
        var response = books.Select(b => new BookResponse(b.ISBN, b.Title, b.Author, b.Pages)).ToList().AsReadOnly();
        return response;
    }
}