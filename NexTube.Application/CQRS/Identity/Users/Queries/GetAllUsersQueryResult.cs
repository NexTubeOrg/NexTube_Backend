using NexTube.Application.Models.Lookups;
using NexTube.Domain.Entities;

namespace NexTube.Application.CQRS.Identity.Users.Queries
{
    public class GetAllUsersQueryResult
    {
        public IEnumerable<UserLookup> Users { get; set; } = null!;
    }
}
