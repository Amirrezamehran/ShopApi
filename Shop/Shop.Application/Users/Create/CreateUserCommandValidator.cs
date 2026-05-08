using Common.Application.Validation;
using Common.Application.Validation.FluentValidations;
using FluentValidation;

namespace Shop.Application.Users.Create
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(u => u.PhoneNumber).ValidPhoneNumber();
            RuleFor(u => u.Email).EmailAddress().WithMessage("ایمیل نامعتبر است");

            RuleFor(u => u.Password)
                .NotEmpty().WithMessage(ValidationMessages.required("کلمه عبور"))
                .NotNull().WithMessage(ValidationMessages.required("کلمه عبور"))
                .MinimumLength(4).WithMessage("کلمه عبور باید بیشتر از 4 کاراکتر باشد");
        }
    }
}
