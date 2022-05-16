using System.Data;
using CRUD.Models;
using CRUD.Models.Validators;

namespace CRUD.test;

public class UnitTests
{
    [Fact]
    public void BookData_should_contain_properties()
    {
        var tBookData = new BookData();

        Assert.True(tBookData.GetType().GetProperty("Id") != null);
        Assert.IsType<int>(tBookData.Id);

        Assert.True(tBookData.GetType().GetProperty("ISBN") != null);
        Assert.IsType<Guid>(tBookData.ISBN);

        Assert.True(tBookData.GetType().GetProperty("Title") != null);
        Assert.IsType<string>(tBookData.Title);

        Assert.True(tBookData.GetType().GetProperty("Author") != null);
        Assert.IsType<string>(tBookData.Author);

        Assert.True(tBookData.GetType().GetProperty("Pages") != null);
        Assert.IsType<int>(tBookData.Pages);

        Assert.True(tBookData.GetType().GetProperty("CreatedAt") != null);
        Assert.IsType<DateTime>(tBookData.CreatedAt);

        Assert.True(tBookData.GetType().GetProperty("ModifiedAt") != null);
        Assert.IsType<DateTime>(tBookData.ModifiedAt);
    }

    [Fact]
    public void AddBookRequest_should_contain_properties()
    {
        var tISBN = Guid.NewGuid();
        var tTitle = "test-title";
        var tAuthor = "test-ticker";
        var tPages = 123;
        var tAddBookRequest = new AddBookRequest()
        {
            ISBN = tISBN,
            Title = tTitle,
            Author = tAuthor,
            Pages = tPages
        };

        Assert.True(tAddBookRequest.GetType().GetProperty("ISBN") != null);
        Assert.IsType<Guid>(tAddBookRequest.ISBN);

        Assert.True(tAddBookRequest.GetType().GetProperty("Title") != null);
        Assert.IsType<string>(tAddBookRequest.Title);

        Assert.True(tAddBookRequest.GetType().GetProperty("Author") != null);
        Assert.IsType<string>(tAddBookRequest.Author);

        Assert.True(tAddBookRequest.GetType().GetProperty("Pages") != null);
        Assert.IsType<int>(tAddBookRequest.Pages);
    }

    [Fact]
    public void BookResponse_should_contain_properties()
    {
        var tISBN = Guid.NewGuid();
        var tTitle = "test-title";
        var tAuthor = "test-ticker";
        var tPages = 123;
        var tBookResponse = new BookResponse(tISBN, tTitle, tAuthor, tPages);

        Assert.True(tBookResponse.GetType().GetProperty("ISBN") != null);
        Assert.IsType<Guid>(tBookResponse.ISBN);

        Assert.True(tBookResponse.GetType().GetProperty("Title") != null);
        Assert.IsType<string>(tBookResponse.Title);

        Assert.True(tBookResponse.GetType().GetProperty("Author") != null);
        Assert.IsType<string>(tBookResponse.Author);

        Assert.True(tBookResponse.GetType().GetProperty("Pages") != null);
        Assert.IsType<int>(tBookResponse.Pages);
    }

    [Fact]
    public void AddBookRequestValidator_should_validate_input()
    {
        var tISBN = Guid.NewGuid();
        var tTitle = "test-title";
        var tAuthor = "test-ticker";
        var tPages = 123;

        var tValidator = new AddBookRequestValidator();

        // should be valid
        var tAddBookRequest = new AddBookRequest()
        {
            ISBN = tISBN,
            Title = tTitle,
            Author = tAuthor,
            Pages = tPages
        };
        var tResult = tValidator.Validate(tAddBookRequest);
        Assert.True(tResult.IsValid);

        /// UUID not empty
        tAddBookRequest = new AddBookRequest()
        {
            ISBN = Guid.Empty,
            Title = tTitle,
            Author = tAuthor,
            Pages = tPages
        };
        tResult = tValidator.Validate(tAddBookRequest);
        Assert.False(tResult.IsValid);
        Assert.Equal(tResult.Errors.Select(e => e.ErrorMessage).First(), "ISBN must not be empty.");

        /// Title not empty
        tAddBookRequest = new AddBookRequest()
        {
            ISBN = tISBN,
            Title = string.Empty,
            Author = tAuthor,
            Pages = tPages
        };
        tResult = tValidator.Validate(tAddBookRequest);
        Assert.False(tResult.IsValid);
        Assert.Equal(tResult.Errors.Select(e => e.ErrorMessage).First(), "Title must not be empty.");

        /// Author not empty
        tAddBookRequest = new AddBookRequest()
        {
            ISBN = tISBN,
            Title = tTitle,
            Author = string.Empty,
            Pages = tPages
        };
        tResult = tValidator.Validate(tAddBookRequest);
        Assert.False(tResult.IsValid);
        Assert.Equal(tResult.Errors.Select(e => e.ErrorMessage).First(), "Author must not be empty.");

        /// Pages not < 0 
        tAddBookRequest = new AddBookRequest()
        {
            ISBN = tISBN,
            Title = tTitle,
            Author = tAuthor,
            Pages = -1
        };
        tResult = tValidator.Validate(tAddBookRequest);
        Assert.False(tResult.IsValid);
        Assert.Equal(tResult.Errors.Select(e => e.ErrorMessage).First(), "Must have more than zero pages.");
    }
}