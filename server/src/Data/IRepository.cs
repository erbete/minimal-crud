using CRUD.Features.Books.Contracts.DTO;

namespace CRUD.Data;

public interface IRepository
{
    Task<BookData> GetBookAsync(Guid isbn);
    Task<List<BookData>> ListBooksAsync();
    Task AddBookAsync(BookData book);
    Task RemoveBookAsync(Guid isbn);
    Task<bool> IsBookRecordRemovedAsync(Guid isbn);
}
