using System.Data;
using CRUD.Data.Queries;
using CRUD.Models;
using Dapper;

namespace CRUD.Data;

public class Repository : IRepository
{
    private readonly IDbConnection _db;
    private readonly IBookQueries _bookQueries;

    public Repository(IDbConnection db, IBookQueries bookQueries)
    {
        // adhering to psql defaults
        Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;

        _db = db;
        _bookQueries = bookQueries;
    }

    public async Task<BookData> GetBookAsync(Guid isbn) =>
        await _db.QueryFirstOrDefaultAsync<BookData>(_bookQueries.GetByISBN, new { ISBN = isbn });

    public async Task<List<BookData>> ListBooksAsync() =>
        (await _db.QueryAsync<BookData>(_bookQueries.GetAll)).AsList();

    public async Task AddBookAsync(BookData newBook)
    {
        // if not already added; add the book 
        var book = await GetBookAsync(newBook.ISBN);
        if (book is null)
        {
            await _db.ExecuteAsync(_bookQueries.Insert, newBook);
            return;
        }

        // do nothing if not modified
        bool isModified =
            book.Title != newBook.Title ||
            book.Author != newBook.Author ||
            book.Pages != newBook.Pages;
        if (!isModified) return;

        // if the book is not null and modified; add a new snapshot of it
        // this does NOT overwrite or change any previous snapshots
        await _db.ExecuteAsync(
                _bookQueries.Update, new
                {
                    book.Id,
                    newBook.Title,
                    newBook.Author,
                    newBook.Pages
                }
        );
    }

    // this does not remove data from the db,
    // instead the data is referenced inside the tombstone/removed table
    public async Task RemoveBookAsync(Guid isbn)
    {
        bool isRemoved = await IsBookRecordRemovedAsync(isbn);
        if (isRemoved) return; // do nothing

        await _db.ExecuteAsync(_bookQueries.Remove, new { ISBN = isbn });
    }

    public async Task<bool> IsBookRecordRemovedAsync(Guid isbn) =>
        (await _db.ExecuteScalarAsync<int>(_bookQueries.IsRecordRemoved, new { ISBN = isbn })) > 0;
}