using MediatR;

namespace NexTube.Application.CQRS.Videos.Queries.GetRandomVideo
{
    public class GetRandomVideoQuery : IRequest<GetRandomVideoQueryResult>
    {
    }
}
