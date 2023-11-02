namespace CRUD.Features.Books.Contracts.DTO;

public record BookResponse(Guid ISBN, string Title, string Author, int Pages);