using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EamaShop.Identity.Services
{
    public interface IRegisterService
    {

        Task RegisterAsync(string account, string password,
            CancellationToken cancellationToken = default(CancellationToken));
    }
}
