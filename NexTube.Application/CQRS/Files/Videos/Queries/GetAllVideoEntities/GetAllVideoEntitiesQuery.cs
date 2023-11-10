using MediatR;

namespace NexTube.Application.CQRS.Files.Videos.Queries.GetAllVideoEntities
{
    public class GetAllVideoEntitiesQuery : IRequest<GetAllVideoEntitiesQueryVm>
    {
    }
}
