using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EamaShop.Identity.Services.Respository
{
    public interface IUnitOfWork
    {
        Task<int> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
