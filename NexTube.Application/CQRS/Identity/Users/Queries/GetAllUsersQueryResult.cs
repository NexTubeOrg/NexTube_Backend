using NexTube.Domain.Entities;

namespace NexTube.Application.CQRS.Identity.Users.Queries
{
    public class GetAllUsersQueryResult
    {
        public IEnumerable<ApplicationUser> Users { get; set; } = null!;
    }
}
