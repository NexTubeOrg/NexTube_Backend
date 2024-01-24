using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexTube.Application.CQRS.Identity.Users.Commands.VerifyMail
{
    public class VerifyMailCommand : IRequest<VerifyMailCommandResult>
    {
        public string VerificationToken{ get; set; } = null!;

        public string SecretPhrase { get; set; } = null!;

        public int UserId { get; set; }
    }
}
