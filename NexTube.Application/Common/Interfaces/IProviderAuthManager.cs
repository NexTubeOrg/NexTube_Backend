using NexTube.Application.Common.Models;
using NexTube.Application.Models.Lookups;

namespace NexTube.Application.Common.Interfaces
{
    public interface IProviderAuthManager {
        Task<(Result Result, UserLookup User)> AuthenticateAsync(
            string providerName, string providerToken);
    }
}
