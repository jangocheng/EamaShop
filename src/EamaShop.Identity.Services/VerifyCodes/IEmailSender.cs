using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EamaShop.Identity.Services
{
    public interface IEmailSender
    {
        Task SendAsync(string email, string content, CancellationToken cancellationToken = default(CancellationToken));
    }
}
