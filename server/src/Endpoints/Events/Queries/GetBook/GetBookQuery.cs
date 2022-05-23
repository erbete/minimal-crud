using CRUD.Models;
using MediatR;

namespace CRUD.Endpoints.Events.Queries;

public record GetBookQuery(Guid ISBN) : IRequest<BookResponse>;