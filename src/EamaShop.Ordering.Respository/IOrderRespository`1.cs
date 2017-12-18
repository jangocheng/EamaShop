using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EamaShop.Ordering.Respository
{
    public interface IOrderRespository : IDisposable
    {
        Task<Order> FindByIdAsync(long id);

        Task<Order> FindByNumberAsync(string orderNumber);

        Task<Order> AddAsync(Order order);
        /// <summary>
        /// 获取当前仓储所属的工作单元
        /// </summary>
        IUnitOfWork UnitOfWork { get; }
    }
}
