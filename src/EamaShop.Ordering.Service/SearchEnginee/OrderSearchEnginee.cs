using EamaShop.Infrastructures;
using EamaShop.Ordering.Respository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EamaShop.Ordering.Service
{
    public class OrderSearchEnginee : ISearchEnginee<OrderListModel, OrderSearchConditions>
    {
        private readonly OrderContext _context;
        public OrderSearchEnginee(OrderContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        async Task<(int Total, IEnumerable<OrderListModel>)> ISearchEnginee<OrderListModel, OrderSearchConditions>.SearchAsync(OrderSearchConditions conditions)
        {
            if (conditions == null)
            {
                throw new ArgumentNullException(nameof(conditions));
            }

            if (conditions.PageSize == 0)
            {
                return (0, Enumerable.Empty<OrderListModel>());
            }

            IQueryable<Order> querybase = _context.Order;
            if (conditions.BuyerId != null)
            {
                querybase = querybase.Where(x => x.UId == conditions.BuyerId);
            }
            if (conditions.StoreId != null)
            {
                querybase = querybase.Where(x => x.StoreId == conditions.StoreId);
            }
            var total = await querybase.CountAsync();

            var result = await querybase
                .Include(x => x.Products)
                .Skip((conditions.PageIndex - 1) * conditions.PageSize)
                .Take(conditions.PageSize).Select(x => new OrderListModel())
                .ToArrayAsync();

            return (total, result);
        }
    }
}
