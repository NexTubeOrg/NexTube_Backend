using NexTube.Application.Common.Models;
using NexTube.Application.Models.Lookups;

namespace NexTube.Application.Common.Interfaces
{
    public interface ITokenVerificator {
        Task<(Result Result, UserLookup User)> VerifyTokenAsync(string providerToken);
    }
}
