namespace CRUD.Models;

public class AddBookRequest
{
    public Guid ISBN { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public int Pages { get; set; }
}