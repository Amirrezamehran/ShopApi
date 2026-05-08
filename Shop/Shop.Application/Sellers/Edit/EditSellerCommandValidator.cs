using Common.Application.Validation;
using Common.Application.Validation.FluentValidations;
using FluentValidation;

namespace Shop.Application.Sellers.Edit
{
    public class EditSellerCommandValidator : AbstractValidator<EditSellerCommand>
    {
        public EditSellerCommandValidator()
        {
            RuleFor(s => s.ShopName).NotEmpty().WithMessage(ValidationMessages.required("نام فروشگاه"));
            RuleFor(s => s.ShopName).NotEmpty().WithMessage(ValidationMessages.required("کدملی")).ValidNationalId();
        }
    }
}
