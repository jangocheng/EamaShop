using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EamaShop.Infrastructures.HttpStandard
{
    public interface IHttpClient<TMicroserviceDescriptor>
        where TMicroserviceDescriptor : MicroserviceDescriptor
    {
        TMicroserviceDescriptor MicroserviceDescriptor { get; }
        /// <summary>
        /// 可以是相对路径，也可以是绝对路径，向指定的微服务发起 GET 请求，通常不需要直接在代码里调用该方法
        /// </summary>
        /// <param name="requestUri">请求的地址</param>
        /// <returns></returns>
        Task<HttpResponseMessage> GetAsync(string requestUri,
            CancellationToken cancellationToken=default(CancellationToken));

        Task<HttpResponseMessage> PostAsync<TParameter>(string requestUri,TParameter parameters, 
            CancellationToken cancellationToken = default(CancellationToken));

        Task<HttpResponseMessage> PutAsync<TParameter>(string requestUri, TParameter parameters, 
            CancellationToken cancellationToken = default(CancellationToken));

        Task<HttpResponseMessage> DeleteAsync(string requestUri, 
            CancellationToken cancellationToken = default(CancellationToken));
    }
}
