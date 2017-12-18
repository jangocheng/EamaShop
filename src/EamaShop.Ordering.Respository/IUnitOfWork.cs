using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EamaShop.Ordering.Respository
{
    public interface IUnitOfWork : IDisposable
    {
        Task<bool> SaveEntitiesAsync();

        Task<int> SaveChangesAsync();
    }
}
