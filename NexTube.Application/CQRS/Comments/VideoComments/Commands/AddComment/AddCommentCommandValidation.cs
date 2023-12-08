using FluentValidation;

namespace NexTube.Application.CQRS.Comments.VideoComments.Commands.AddComment {
    public class AddCommentReplyCommandValidation : AbstractValidator<AddCommentCommand> {
        public AddCommentReplyCommandValidation() {
            RuleFor(c => c.Content)
                .NotEmpty()
                .MinimumLength(1)
                .MaximumLength(500);

            RuleFor(c => c.VideoId)
                .NotEmpty();
        }
    }
}
