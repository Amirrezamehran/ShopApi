using Common.Application.Validation;
using Common.Application.Validation.FluentValidations;
using FluentValidation;

namespace Shop.Application.Orders.Checkout
{
    public class CheckoutOrderCommandValidator : AbstractValidator<CheckoutOrderCommand>
    {
        public CheckoutOrderCommandValidator()
        {
            RuleFor(a => a.Name).NotNull().NotEmpty().WithMessage(ValidationMessages.required("نام"));
            RuleFor(a => a.Family).NotNull().NotEmpty().WithMessage(ValidationMessages.required("نام خانوادگی"));
            RuleFor(a => a.Province).NotNull().NotEmpty().WithMessage(ValidationMessages.required("استان"));
            RuleFor(a => a.City).NotNull().NotEmpty().WithMessage(ValidationMessages.required("شهر"));
            RuleFor(a => a.PostalAddress).NotNull().NotEmpty().WithMessage(ValidationMessages.required("آدرس پستی"));
            RuleFor(a => a.PostalCode).NotNull().NotEmpty().WithMessage(ValidationMessages.required("کد پستی"));

            RuleFor(a => a.PhoneNumber).NotNull().NotEmpty().WithMessage(ValidationMessages.required("تلفن همراه"))
                .MaximumLength(11).WithMessage("شماره مبایل نامعتبر است")
                .MinimumLength(11).WithMessage("شماره مبایل نامعتبر است");

            RuleFor(a => a.NationalCode).NotNull().NotEmpty().WithMessage(ValidationMessages.required("کدملی"))
                .MaximumLength(10).WithMessage("کدملی نامعتبر است")
                .MinimumLength(10).WithMessage("کدملی نامعتبر است")
                .ValidNationalId();
        }
    }
}
