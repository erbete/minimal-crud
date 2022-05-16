using CRUD.Models;

namespace CRUD.Data;

public interface IRepository
{
    Task<BookData> GetBookAsync(Guid isbn);
    Task<List<BookData>> ListBooksAsync();
    Task<BookData> AddBookAsync(BookData book);
    Task RemoveBookAsync(Guid isbn);
    Task<bool> IsBookRecordRemovedAsync(Guid isbn);
}
