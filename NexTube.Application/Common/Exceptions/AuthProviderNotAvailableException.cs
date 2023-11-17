using Ardalis.GuardClauses;

namespace NexTube.Application.Common.Exceptions {
    public class AuthProviderNotAvailableException : NotFoundException {
        private const string AUTH_PROVIDER = nameof(AUTH_PROVIDER);
        public AuthProviderNotAvailableException(string providerName) : base(AUTH_PROVIDER, providerName) {
        }
    }
}
