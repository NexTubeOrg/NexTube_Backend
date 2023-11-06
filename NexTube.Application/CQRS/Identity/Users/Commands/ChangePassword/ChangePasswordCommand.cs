using MediatR;
using NexTube.Application.Common.Models;

namespace NexTube.Application.CQRS.Identity.Users.Commands.ChangePassword {
    public class ChangePasswordCommand : IRequest<Result> {
        public string Password { get; set; } = null!;
        public int UserId { get; set; }
        public string NewPassword { get; set; } = null!;
    }
}
