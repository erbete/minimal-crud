using CRUD.Models;
using MediatR;

namespace CRUD.Routes.Events.Queries;

public record ListAllBooksQuery() : IRequest<IReadOnlyList<BookResponse>>;