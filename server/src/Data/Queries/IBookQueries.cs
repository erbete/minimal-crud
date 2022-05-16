namespace CRUD.Data.Queries;

public interface IBookQueries
{
    string GetAll { get; }
    string GetByISBN { get; }
    string Insert { get; }
    string Update { get; }
    string Remove { get; }
    string IsRecordRemoved { get; }
}
