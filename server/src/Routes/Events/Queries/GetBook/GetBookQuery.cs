using CRUD.Models;
using MediatR;

namespace CRUD.Routes.Events.Queries;

public record GetBookQuery(Guid ISBN) : IRequest<BookResponse>;