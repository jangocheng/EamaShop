using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EamaShop.Infrastructures.HttpStandard
{

    public interface IHttpClient
    {
        Task<HttpResponseMessage> GetAsync(string requestUri,
            string authorizationToken = null,
            string authorizationMethod = "Bearer",
            CancellationToken cancellationToken = default(CancellationToken));

        Task<HttpResponseMessage> DeleteAsync(string requestUri,
            string authorizationToken = null,
            string authorizationMethod = "Bearer",
            CancellationToken cancellationToken = default(CancellationToken));

        Task<HttpResponseMessage> PostAsync<Parameter>(string requestUri,
            Parameter parameter,
            string authorizationToken = null,
            string authorizationMethod = "Bearer",
            CancellationToken cancellationToken = default(CancellationToken));

        Task<HttpResponseMessage> PutAsync<Parameter>(string requestUri,
            Parameter parameter,
            string authorizationToken = null,
            string authorizationMethod = "Bearer",
            CancellationToken cancellationToken = default(CancellationToken));
    }
}
