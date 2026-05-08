using Common.Application.Validation;
using Common.Application.Validation.FluentValidations;
using FluentValidation;

namespace Shop.Application.Sellers.Create
{
    public class CreateSellerCommandValidator : AbstractValidator<CreateSellerCommand>
    {
        public CreateSellerCommandValidator()
        {
            RuleFor(s => s.ShopName).NotEmpty().WithMessage(ValidationMessages.required("نام فروشگاه"));
            RuleFor(s => s.ShopName).NotEmpty().WithMessage(ValidationMessages.required("کدملی")).ValidNationalId();
        }
    }
}
