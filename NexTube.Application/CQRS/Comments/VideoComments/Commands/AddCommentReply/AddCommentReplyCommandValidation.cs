using FluentValidation;

namespace NexTube.Application.CQRS.Comments.VideoComments.Commands.AddCommentReply {
    public class AddCommentReplyCommandValidation : AbstractValidator<AddCommentReplyCommand> {
        public AddCommentReplyCommandValidation() {
            RuleFor(c => c.Content)
                .NotEmpty()
                .MinimumLength(1)
                .MaximumLength(500);

            RuleFor(c => c.VideoId)
                .NotEmpty();

            RuleFor(c => c.ReplyToCommentId)
                .NotEmpty();
        }
    }
}
