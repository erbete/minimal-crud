using CRUD.Data;
using CRUD.Endpoints.Events.Commands;
using MediatR;

namespace CRUD.Endpoints.Events.Command;

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