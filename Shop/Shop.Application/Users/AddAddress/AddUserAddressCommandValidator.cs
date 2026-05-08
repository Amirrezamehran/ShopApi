using Common.Application.Validation;
using Common.Application.Validation.FluentValidations;
using FluentValidation;

namespace Shop.Application.Users.AddAddress
{
    public class AddUserAddressCommandValidator : AbstractValidator<AddUserAddressCommand>
    {
        public AddUserAddressCommandValidator()
        {
            RuleFor(u => u.Province).NotEmpty().WithMessage(ValidationMessages.required("استان"));
            RuleFor(u => u.City).NotEmpty().WithMessage(ValidationMessages.required("شهر"));
            RuleFor(u => u.PostalCode).NotEmpty().WithMessage(ValidationMessages.required("کدپستی"));
            RuleFor(u => u.PostalAddress).NotEmpty().WithMessage(ValidationMessages.required("آدرس پستی"));
            RuleFor(u => u.Name).NotEmpty().WithMessage(ValidationMessages.required("نام"));
            RuleFor(u => u.Family).NotEmpty().WithMessage(ValidationMessages.required("نام خانوادگی"));
            RuleFor(u => u.NationalCode).NotEmpty().WithMessage(ValidationMessages.required("کدملی")).ValidNationalId();
        }
    }
}
