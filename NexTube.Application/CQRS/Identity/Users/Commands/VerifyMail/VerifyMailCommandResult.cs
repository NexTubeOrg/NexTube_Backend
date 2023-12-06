using NexTube.Application.Common.Models;
using NexTube.Application.Models.Lookups;

namespace NexTube.Application.CQRS.Identity.Users.Commands.VerifyMail
{
    public class VerifyMailCommandResult
    {
        public Result Result { get; set; } = null!;
        public string Token { get; set; } = null!;
        public UserLookup User { get; set; } = null!;
    }
}
