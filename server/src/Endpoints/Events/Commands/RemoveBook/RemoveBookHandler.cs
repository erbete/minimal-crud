using CRUD.Data;
using CRUD.Endpoints.Events.Commands;
using MediatR;

namespace CRUD.Endpoints.Events.Command;

public class RemoveBookHandler : IRequestHandler<RemoveBookCommand>
{
    private readonly IRepository _repository;

    public RemoveBookHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(RemoveBookCommand request, CancellationToken cancellationToken)
    {
        await _repository.RemoveBookAsync(request.ISBN);
        return Unit.Value;
    }
}