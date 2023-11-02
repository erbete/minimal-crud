using MediatR;

namespace CRUD.Routes.Events.Commands;

public record RemoveBookCommand(Guid ISBN) : IRequest;