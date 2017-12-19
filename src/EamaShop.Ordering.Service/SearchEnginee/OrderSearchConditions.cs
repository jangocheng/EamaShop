using EamaShop.Infrastructures;
using System;
using System.Collections.Generic;
using System.Text;

namespace EamaShop.Ordering.Service
{
    public class OrderSearchConditions : ISearchableCondition
    {
        private int _pageIndex = 1;
        public int PageIndex { get => _pageIndex; set => _pageIndex = value < 1 ? 1 : value; }
        private int _pageSize = 0;
        public int PageSize { get => _pageSize; set => _pageSize = value < 0 ? 0 : value; }
        private long? _buyerId = null;
        public long? BuyerId { get => _buyerId; set => _buyerId = value == null || value < 1 ? null : value; }
        private long? _storeId = null;
        public long? StoreId { get => _storeId; set => _storeId = value == null || value < 1 ? null : value; }
    }
}
