using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EamaShop.Identity.Services
{
    /// <summary>
    /// 短信发送
    /// </summary>
    public interface ISmsSender
    {
        /// <summary>
        /// Send Sms Message to given phone.
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        Task SendAsync(string phone, string content);
    }
}
