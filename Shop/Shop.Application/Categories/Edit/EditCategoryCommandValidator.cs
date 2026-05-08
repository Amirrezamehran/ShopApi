using Common.Application.Validation;
using FluentValidation;

namespace Shop.Application.Categories.Edit
{
    public class EditCategoryCommandValidator : AbstractValidator<EditCategoryCommand>
    {
        public EditCategoryCommandValidator()
        {
            RuleFor(c => c.Title).NotNull().NotEmpty().WithMessage(ValidationMessages.required("عنوان"));
            RuleFor(c => c.Slug).NotNull().NotEmpty().WithMessage(ValidationMessages.required("Slug"));
        }
    }
}
