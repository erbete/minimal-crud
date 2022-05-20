using CRUD.Models;
using MediatR;

namespace CRUD.Endpoints.Events.Commands;

public record AddBookCommand(AddBookRequest newBook) : IRequest<BookResponse>;