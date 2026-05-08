using Common.Application.Validation;
using Common.Application.Validation.FluentValidations;
using FluentValidation;


namespace Shop.Application.Products.Edit
{
    public class EditProductCommandValidator : AbstractValidator<EditProductCommand>
    {
        public EditProductCommandValidator()
        {
            RuleFor(p => p.Title).NotEmpty().WithMessage(ValidationMessages.required("عنوان"));
            RuleFor(p => p.Slug).NotEmpty().WithMessage(ValidationMessages.required("Slug"));
            RuleFor(p => p.Description).NotEmpty().WithMessage(ValidationMessages.required("توضیحات"));
            RuleFor(p => p.ImageFile).JustImageFile();
        }
    }
}
