using MediatR;
using NexTube.Domain.Entities;

namespace NexTube.Application.CQRS.Identity.Users.Commands.ChangeBanner
{
    public class ChangeBannerCommand : IRequest<Unit>
    {
        public Stream? BannerStream { get; set; }
        public ApplicationUser Requester { get; set; }
    }
}
