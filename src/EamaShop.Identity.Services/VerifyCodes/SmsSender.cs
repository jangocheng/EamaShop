using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EamaShop.Identity.Services
{
    public class SmsSender : ISmsSender
    {
        public Task SendAsync(string phone, string content)
        {
            if (phone == null)
            {
                throw new ArgumentNullException(nameof(phone));
            }

            if (content == null)
            {
                throw new ArgumentNullException(nameof(content));
            }

            return Task.CompletedTask;
        }
    }
}
