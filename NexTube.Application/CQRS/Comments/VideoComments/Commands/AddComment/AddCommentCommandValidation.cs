using FluentValidation;

namespace NexTube.Application.CQRS.Comments.VideoComments.Commands.AddComment
{
    public class AddCommentCommandValidation : AbstractValidator<AddCommentCommand>
    {
        public AddCommentCommandValidation()
        {
            RuleFor(c => c.Content)
                .NotEmpty()
                .MinimumLength(1)
                .MaximumLength(500);

            RuleFor(c => c.VideoId)
                .NotEmpty();
        }
    }
}
