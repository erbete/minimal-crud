using CRUD.Data;
using CRUD.Routes.Events.Commands;
using MediatR;

namespace CRUD.Routes.Events.Command;

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