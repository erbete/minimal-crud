using MediatR;

namespace CRUD.Endpoints.Events.Commands;

public record RemoveBookCommand(Guid ISBN) : IRequest;