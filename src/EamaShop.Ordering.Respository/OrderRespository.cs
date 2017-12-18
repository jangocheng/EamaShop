using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EamaShop.Ordering.Respository
{
    public class OrderRespository : IOrderRespository, IUnitOfWork
    {
        private readonly OrderContext _context;
        public OrderRespository(OrderContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public IUnitOfWork UnitOfWork => this;

        public async Task<Order> AddAsync(Order order, CancellationToken cancellationToken = default(CancellationToken))
        {
            var entry = await _context.Order.AddAsync(order);

            return entry.Entity;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public  Task<Order> FindByIdAsync(long id, CancellationToken cancellationToken = default(CancellationToken))
        {
            return _context.Order.Include(x => x.Products).FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public Task<Order> FindByNumberAsync(string orderNumber, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (orderNumber == null)
            {
                throw new ArgumentNullException(nameof(orderNumber));
            }

            return _context.Order.Include(x => x.Products).FirstOrDefaultAsync(x => x.OrderNumber == orderNumber, cancellationToken);
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return await _context.SaveChangesAsync();
        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return await SaveChangesAsync() > 0;
        }
    }
}
