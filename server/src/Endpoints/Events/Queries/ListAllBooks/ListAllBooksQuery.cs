using CRUD.Models;
using MediatR;

namespace CRUD.Endpoints.Events.Queries;

public record ListAllBooksQuery() : IRequest<IReadOnlyList<BookResponse>>;