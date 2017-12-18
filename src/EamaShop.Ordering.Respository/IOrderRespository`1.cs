using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EamaShop.Ordering.Respository
{
    public interface IOrderRespository : IDisposable
    {
        Task<Order> FindByIdAsync(long id, CancellationToken cancellationToken = default(CancellationToken));

        Task<Order> FindByNumberAsync(string orderNumber, CancellationToken cancellationToken = default(CancellationToken));

        Task<Order> AddAsync(Order order, CancellationToken cancellationToken = default(CancellationToken));
        /// <summary>
        /// 获取当前仓储所属的工作单元
        /// </summary>
        IUnitOfWork UnitOfWork { get; }
    }
}
