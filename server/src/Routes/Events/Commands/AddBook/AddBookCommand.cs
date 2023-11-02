using CRUD.Models;
using MediatR;

namespace CRUD.Routes.Events.Commands;

public record AddBookCommand(AddBookRequest NewBook) : IRequest<BookResponse>;