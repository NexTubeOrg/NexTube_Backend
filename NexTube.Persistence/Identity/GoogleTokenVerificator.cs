using Google.Apis.Auth;
using Microsoft.Extensions.Configuration;
using NexTube.Application.Common.Interfaces;
using NexTube.Application.Common.Models;
using NexTube.Application.Models.Lookups;

namespace NexTube.Persistence.Identity
{

    [AuthProviderVerificator(ProviderName = "google")]
    public class GoogleTokenVerificator : ITokenVerificator {
        private readonly IConfiguration configuration;

        public GoogleTokenVerificator(IConfiguration configuration) {
            this.configuration = configuration;
        }

        public async Task<(Result Result, UserLookup User)> VerifyTokenAsync(string providerToken) {
            string clientID = configuration["GoogleOAuth:ClientId"] ?? "";
            var settings = new GoogleJsonWebSignature.ValidationSettings() {
                Audience = new List<string>() { clientID }
            };

            var payload = await GoogleJsonWebSignature.ValidateAsync(providerToken, settings);

            var user = new UserLookup() {
                Email = payload.Email,
                FirstName = payload.GivenName,
                LastName = payload.FamilyName,
                ChannelPhoto = payload.Picture
            };

            return (Result.Success(), user);
        }
    }
}
