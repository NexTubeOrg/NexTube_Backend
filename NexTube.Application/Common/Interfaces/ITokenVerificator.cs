using NexTube.Application.Common.Models;
using NexTube.Application.CQRS.Identity.Users.Commands.SignInUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexTube.Application.Common.Interfaces {
    public interface ITokenVerificator {
        Task<(Result Result, UserLookup User)> VerifyTokenAsync(string providerToken);
    }
}
