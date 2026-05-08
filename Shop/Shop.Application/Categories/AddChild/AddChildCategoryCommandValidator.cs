using Common.Application.Validation;
using FluentValidation;

namespace Shop.Application.Categories.AddChild
{
    public class AddChildCategoryCommandValidator : AbstractValidator<AddChildCategoryCommand>
    {
        public AddChildCategoryCommandValidator()
        {
            RuleFor(c => c.Title).NotNull().NotEmpty().WithMessage(ValidationMessages.required("عنوان"));
            RuleFor(c => c.Slug).NotNull().NotEmpty().WithMessage(ValidationMessages.required("Slug"));
        }
    }
}
