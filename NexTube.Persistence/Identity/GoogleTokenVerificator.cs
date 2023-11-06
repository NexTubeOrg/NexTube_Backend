using Google.Apis.Auth;
using Microsoft.Extensions.Configuration;
using NexTube.Application.Common.Interfaces;
using NexTube.Application.Common.Models;
using NexTube.Application.CQRS.Identity.Users.Commands.SignInUser;

namespace NexTube.Persistence.Identity {

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
            };

            return (Result.Success(), user);
        }
    }
}
