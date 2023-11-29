using FluentValidation;

namespace NexTube.Application.CQRS.Comments.VideoComments.Queries.GetCommentRepliesList {
    public class GetCommentRepliesListQueryValidation : AbstractValidator<GetCommentRepliesListQuery> {
        public GetCommentRepliesListQueryValidation() {
            RuleFor(c => c.VideoId)
                .NotEmpty();

            RuleFor(c => c.RootCommentId)
                .NotEmpty();

            RuleFor(c => c.Page)
                .NotEmpty()
                .GreaterThan(0);

            RuleFor(c => c.PageSize)
                .NotEmpty()
                .GreaterThan(0);
        }
    }
}
