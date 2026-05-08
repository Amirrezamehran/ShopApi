using Common.Application.Validation;
using FluentValidation;
using Shop.Application.Users.ChargeWallet;

public class ChargeUserWalletCommandValidator : AbstractValidator<ChargeUserWalletCommand>
{
    public ChargeUserWalletCommandValidator()
    {
        RuleFor(w => w.Description).NotEmpty().WithMessage(ValidationMessages.required("توضیحات"));
        RuleFor(w => w.Price).GreaterThanOrEqualTo(1000); // از هزار به بالا میتونه ولت رو شارژ کنه
    }
}