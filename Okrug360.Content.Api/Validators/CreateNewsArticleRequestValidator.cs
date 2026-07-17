using FluentValidation;
using Okrug360.Content.Api.Dtos;

namespace Okrug360.Content.Api.Validators
{
    public sealed class CreateNewsArticleRequestValidator
        :AbstractValidator<CreateNewsArticleRequest>
    {
        public CreateNewsArticleRequestValidator() {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(200).WithMessage("Title must be at most 200 characters.");

            RuleFor(x => x.Summary)
                .NotEmpty().WithMessage("Summary is required.")
                .MaximumLength(500).WithMessage("Title must be at 500 charcters");

            RuleFor(x => x.Content)
            .NotEmpty().WithMessage("Content is required.")
            .MaximumLength(10000).WithMessage("Content must be at most 10000 characters.");
        }
    }
}
