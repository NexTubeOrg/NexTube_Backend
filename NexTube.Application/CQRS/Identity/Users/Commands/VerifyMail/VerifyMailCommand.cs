using MediatR;

namespace NexTube.Application.CQRS.Identity.Users.Commands.VerifyMail
{
    public class VerifyMailCommand : IRequest<VerifyMailCommandResult>
    {
        public string Email { get; set; } = null!;
        public string SecretPhrase { get; set; } = null!;
    }
}
