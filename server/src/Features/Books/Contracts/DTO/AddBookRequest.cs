namespace CRUD.Features.Books.Contracts.DTO;

public class AddBookRequest
{
    public string ISBN { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public int Pages { get; set; }
}