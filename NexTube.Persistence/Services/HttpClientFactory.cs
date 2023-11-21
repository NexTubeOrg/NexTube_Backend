using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NexTube.Application.Common.Interfaces;

namespace NexTube.Persistence.Services {
    public class HttpClientFactory : Application.Common.Interfaces.IHttpClientFactory {
        private readonly System.Net.Http.IHttpClientFactory httpClientFactory;

        public HttpClientFactory(System.Net.Http.IHttpClientFactory httpClientFactory) {
            this.httpClientFactory = httpClientFactory;
        }

        public HttpClient CreateClient(string name = "") {
            return httpClientFactory.CreateClient(name);
        }
    }
}
