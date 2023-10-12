using NexTube.Application.Common.Models;

namespace NexTube.Application.CQRS.Identity.Users.Commands.SignInUser   
{
    public class SignInUserCommandResult
    {
        public Result Result { get; set; } = null!;
        public string Token { get; set; } = null!;
        public string FirstName { get; set; } = null!;

    }
}
