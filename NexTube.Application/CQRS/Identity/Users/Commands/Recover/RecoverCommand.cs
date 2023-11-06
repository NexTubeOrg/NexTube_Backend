using MediatR;
using NexTube.Application.Common.Models;

namespace NexTube.Application.CQRS.Identity.Users.Commands.Recover {
    public class RecoverCommand : IRequest<Result> {
        public string Email { get; set; } = null!;
    }
}
