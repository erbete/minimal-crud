using FluentValidation;
using CRUD.Features.Books.Contracts.DTO;

namespace CRUD.Features.Books.Validation;

public class AddBookRequestValidator : AbstractValidator<AddBookRequest>
{
    public AddBookRequestValidator()
    {
        RuleFor(c => c.ISBN)
            .NotNull()
                .WithMessage("ISBN must not be empty.")
            .NotEmpty()
                .WithMessage("ISBN must not be empty.")
            .Must(val => Guid.TryParse(val, out var _))
                .WithMessage("Invalid ISBN format.");

        RuleFor(c => c.Title)
            .NotNull()
                .WithMessage("Title must not be empty.")
            .NotEmpty()
                .WithMessage("Title must not be empty.");

        RuleFor(c => c.Author)
            .NotNull()
                .WithMessage("Author must not be empty.")
            .NotEmpty()
                .WithMessage("Author must not be empty.");

        RuleFor(c => c.Pages)
            .NotNull()
                .WithMessage("Pages must not be empty.")
            .GreaterThan(0)
                .WithMessage("Must have more than zero pages.");
    }
}