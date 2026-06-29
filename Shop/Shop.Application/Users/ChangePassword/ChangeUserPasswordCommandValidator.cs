using Common.Application.Validation;
using FluentValidation;

namespace Shop.Application.Users.ChangePassword
{
    public class ChangeUserPasswordCommandValidator : AbstractValidator<ChangeUserPasswordCommand>
    {
        public ChangeUserPasswordCommandValidator()
        {
            RuleFor(cp => cp.CurrentPassword).NotEmpty().WithMessage(ValidationMessages.required("کلمه عبور فعلی")).MinimumLength(5);
            RuleFor(cp => cp.Password).NotEmpty().WithMessage(ValidationMessages.required("کلمه عبور")).MinimumLength(5);
        }
    }
}
