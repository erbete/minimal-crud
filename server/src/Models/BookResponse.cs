namespace CRUD.Models;

public record BookResponse(Guid ISBN, string Title, string Author, int Pages);