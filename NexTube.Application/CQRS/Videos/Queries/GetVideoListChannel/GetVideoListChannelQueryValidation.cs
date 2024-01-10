using FluentValidation;

namespace NexTube.Application.CQRS.Videos.Queries.GetVideoListChannel
{
    public class GetVideoListChannelQueryValidation : AbstractValidator<GetVideoListChannelQuery>
    {

        public GetVideoListChannelQueryValidation()
        {
            RuleFor(q => q.ChannelId)
                .NotEmpty()
                .GreaterThan(0);

            RuleFor(q => q.Page)
                .NotEmpty()
                .GreaterThan(0);

            RuleFor(q => q.PageSize)
                .NotEmpty()
                .GreaterThan(0);
        }
    }
}
