using FluentValidation;

namespace NexTube.Application.CQRS.Comments.VideoComments.Queries.GetCommentsList
{
    public class GetCommentsListQueryValidation : AbstractValidator<GetCommentsListQuery>
    {
        public GetCommentsListQueryValidation()
        {
            RuleFor(c => c.VideoId)
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
