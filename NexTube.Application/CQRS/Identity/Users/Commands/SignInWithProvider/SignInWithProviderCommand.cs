using MediatR;

namespace NexTube.Application.CQRS.Identity.Users.Commands.SignInWithProvider   
{
    public class SignInWithProviderCommand : IRequest<SignInWithProviderCommandResult>
    {
        public string ProviderToken { get; set; } = null!;
        public string Provider { get; set; } = null!;
    }
}
