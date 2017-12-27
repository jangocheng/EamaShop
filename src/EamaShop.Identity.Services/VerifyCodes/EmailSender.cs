using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EamaShop.Identity.Services
{
    public class EmailSender : IEmailSender
    {
        public Task SendAsync(string email, string content, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (email == null)
            {
                throw new ArgumentNullException(nameof(email));
            }

            if (content == null)
            {
                throw new ArgumentNullException(nameof(content));
            }

            return Task.CompletedTask;
        }
    }
}
