using Common.Application.Validation;
using Common.Application.Validation.FluentValidations;
using FluentValidation;

namespace Shop.Application.Products.AddImage
{
    public class AddProductImageCommandValidator : AbstractValidator<AddProductImageCommand>
    {
        public AddProductImageCommandValidator()
        {
            RuleFor(i => i.ImageFile).NotNull().WithMessage(ValidationMessages.required("عکس")).JustImageFile();
            RuleFor(i => i.Sequence).GreaterThanOrEqualTo(0);
        }
    }
}
