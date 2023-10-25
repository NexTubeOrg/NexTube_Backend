using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NexTube.Application.Common.Interfaces;
using System.Reflection;
using static System.Formats.Asn1.AsnWriter;

namespace NexTube.Persistence.Identity {
    public class TokenVerificatorsFactory {
        private readonly IServiceProvider serviceProvider;

        public TokenVerificatorsFactory(IServiceProvider serviceProvider) {
            this.serviceProvider = serviceProvider;
        }
        public IDictionary<string, ITokenVerificator> CreateVerificators() {
            var dict = new Dictionary<string, ITokenVerificator>();

            // get all registered services-realisers of ITokenVerificator
            var scopedServices = serviceProvider.GetServices<ITokenVerificator>();
           
            foreach ( var scopedService in scopedServices) {
                // get all AuthProviderVerificatorAttribute attributes of type
                var attrs = scopedService
                    .GetType()
                    .GetCustomAttributes(typeof(AuthProviderVerificatorAttribute), false);

                if (attrs.Length == 0)
                    continue;

                // get provider name from attribute from property
                var providerName = ((AuthProviderVerificatorAttribute)attrs[0]).ProviderName??throw new ArgumentNullException("Attr.ProviderName", scopedService.GetType().FullName);

                dict.Add(providerName, scopedService);
            }
            return dict;
        }
    }
}
