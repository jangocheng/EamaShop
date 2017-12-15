using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace EamaShop.Catalog.API.Infrastructures
{
    public static class API
    {
        private static HttpClient _client = new HttpClient();

        public static async Task<StoreDto> GetStoreById(long id,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();

            var response = await _client
                .GetAsync($"http://localhost:5000/api/merchant/{id}", cancellationToken);

            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    // modelstate error
                    var dictionary = JsonConvert.DeserializeObject<IDictionary<string, string>>(content);
                }
                if(response.StatusCode== System.Net.HttpStatusCode.NotFound)
                {
                    return null;
                }

                if(response.StatusCode== System.Net.HttpStatusCode.Forbidden)
                {

                }
            }
            return null;

        }
    }


}
