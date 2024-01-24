using NexTube.Application.Models.Lookups;

namespace NexTube.Application.Common.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(int userId, UserLookup user);
        public string GenerateTokenWithSecretPhrase(string secretPhrase);

        public bool VerifyTokenWithSecretPhrase(string token, string secretPhrase);
    }
}
