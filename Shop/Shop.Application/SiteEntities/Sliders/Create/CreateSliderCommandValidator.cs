using Common.Application.Validation;
using Common.Application.Validation.FluentValidations;
using FluentValidation;

namespace Shop.Application.SiteEntities.Sliders.Create
{
    public class CreateSliderCommandValidator : AbstractValidator<CreateSliderCommand>
    {
        public CreateSliderCommandValidator()
        {
            RuleFor(b => b.ImageFile).NotNull().WithMessage(ValidationMessages.required("عکس")).JustImageFile();
            RuleFor(b => b.Link).NotNull().NotEmpty().WithMessage(ValidationMessages.required("لینک"));
            RuleFor(b => b.Title).NotNull().NotEmpty().WithMessage(ValidationMessages.required("عنوان"));
        }
    }
}
