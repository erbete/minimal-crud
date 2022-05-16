namespace CRUD.Data.Queries;

public class BookQueries : IBookQueries
{
    public string GetAll => @"
        SELECT DISTINCT ON (b.id) 
               b.isbn, 
               bs.title, 
               bs.author, 
               bs.pages, 
               b.created_at, 
               bs.modified_at
        FROM books b
        LEFT JOIN book_removed br
        ON b.id = br.book_id
        LEFT JOIN book_snapshots bs
        ON b.id = bs.book_id
        WHERE br.book_id IS NULL
        ORDER BY b.id, bs.modified_at DESC;
    ";

    public string GetByISBN => @"
        SELECT b.id, 
               b.isbn, 
               bs.title, 
               bs.author, 
               bs.pages, 
               b.created_at, 
               bs.modified_at
        FROM books b
        LEFT JOIN book_removed br
        ON b.id = br.book_id
        LEFT JOIN book_snapshots bs
        ON b.id = bs.book_id
        WHERE b.isbn = @ISBN
        AND br.book_id IS NULL
        ORDER BY bs.modified_at DESC
        LIMIT 1;
    ";

    public string Insert => @"
        WITH parent AS (
            INSERT INTO books (isbn)
            VALUES (@ISBN)
            RETURNING id 
        )
        INSERT INTO book_snapshots (book_id, title, author, pages)
        VALUES ((SELECT id FROM parent), @Title, @Author, @Pages);
    ";

    public string Update => @"
        INSERT INTO book_snapshots (book_id, title, author, pages)
        VALUES (@Id, @Title, @Author, @Pages);
    ";

    public string Remove => @"
        INSERT INTO book_removed (book_id)
        SELECT id 
        FROM books 
        WHERE isbn = @ISBN;
    ";

    public string IsRecordRemoved => @"
        SELECT 1 FROM books b
        LEFT JOIN book_removed br
        ON b.id = br.book_id 
        WHERE b.isbn = @ISBN
        AND br.book_id IS NOT NULL
    ";
}
