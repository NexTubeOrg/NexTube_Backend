using MediatR;

namespace NexTube.Application.CQRS.Identity.Users.Queries
{
    public class GetAllUsersQuery : IRequest<GetAllUsersQueryResult>
    {
    }
}
