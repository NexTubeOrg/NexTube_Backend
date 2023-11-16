using Microsoft.Extensions.Options;
using NexTube.Application.Common.Exceptions;
using NexTube.Application.Common.Interfaces;
using NexTube.Application.Common.Models;
using NexTube.Application.Models.Lookups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexTube.Persistence.Identity
{
    public class OAuth2Manager : IProviderAuthManager {
        private IDictionary<string, ITokenVerificator> _verificators;

        public OAuth2Manager(TokenVerificatorsFactory verificatorsFactory) {
            _verificators = verificatorsFactory.CreateVerificators();
        }

        public async Task<(Result Result, UserLookup User)> AuthenticateAsync(string providerName, string providerToken) {
            if (!_verificators.ContainsKey(providerName))
                throw new AuthProviderNotAvailableException(providerName);

            var provider = _verificators[providerName];

            return await provider.VerifyTokenAsync(providerToken);
        }
    }
}
