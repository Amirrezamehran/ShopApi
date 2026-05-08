using Common.Application.Validation;
using Common.Application.Validation.FluentValidations;
using FluentValidation;

namespace Shop.Application.SiteEntities.Banners.Create
{
    public class CreateBannerCommandValidator : AbstractValidator<CreateBannerCommand>
    {
        public CreateBannerCommandValidator()
        {
            RuleFor(b => b.ImageFile).NotNull().WithMessage(ValidationMessages.required("عکس")).JustImageFile();
            RuleFor(b => b.Link).NotNull().NotEmpty().WithMessage(ValidationMessages.required("لینک"));
        }
    }
}
