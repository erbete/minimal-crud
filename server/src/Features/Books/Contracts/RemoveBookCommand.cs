using MediatR;
using CRUD.Data;

namespace CRUD.Features.Books.Contracts;

public record RemoveBookCommand(Guid ISBN) : IRequest;

public class RemoveBookHandler : IRequest
{
    private readonly IRepository _repository;

    public RemoveBookHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(RemoveBookCommand request, CancellationToken cancellationToken)
    {
        await _repository.RemoveBookAsync(request.ISBN);
    }
}