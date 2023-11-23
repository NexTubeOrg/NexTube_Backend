using MediatR;
using NexTube.Domain.Entities;

namespace NexTube.Application.CQRS.Identity.Users.Queries {
    public class GetUserByIdQuery : IRequest<ApplicationUser> {
        public int UserId { get; set; }
    }
}
