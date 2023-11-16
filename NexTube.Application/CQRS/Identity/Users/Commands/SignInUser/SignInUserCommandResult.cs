using NexTube.Application.Common.Models;
using NexTube.Application.Models.Lookups;

namespace NexTube.Application.CQRS.Identity.Users.Commands.SignInUser
{
    public class SignInUserCommandResult
    {
        public Result Result { get; set; } = null!;
        public string Token { get; set; } = null!;
        public UserLookup User { get; set; } = null!;
    }
}
