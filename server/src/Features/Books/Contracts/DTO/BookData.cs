namespace CRUD.Features.Books.Contracts.DTO;

public class BookData
{
    public int Id { get; set; }
    public Guid ISBN { get; set; }

    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public int Pages { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ModifiedAt { get; set; }
}
