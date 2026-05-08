using Common.Application.Validation;
using FluentValidation;

namespace Shop.Application.Comments.Edit
{
    public class EditCommentCommandValidator : AbstractValidator<EditCommentCommand>
    {
        public EditCommentCommandValidator()
        {
            RuleFor(c => c.Text).NotNull().MinimumLength(5).WithMessage(ValidationMessages.minLength("متن نظر", 5));
        }
    }
}
