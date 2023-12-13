
using MediatR;
using NexTube.Application.CQRS.Comments.VideoComments.Queries.GetCommentsList;
using NexTube.Domain.Entities;

namespace NexTube.Application.Subscriptions.Commands
{
    public class GetSubscriptionQueries : IRequest<GetSubscriptionsListQueryResult>
    {

        public int SubscriptionUserTo { get; set; }
    }
}