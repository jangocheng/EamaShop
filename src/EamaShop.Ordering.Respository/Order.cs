using System;
using System.Collections.Generic;
using System.Text;

namespace EamaShop.Ordering.Respository
{
    public sealed class Order
    {
        public long Id { get; set; }

        public string OrderNumber { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime ModifiedTime { get; set; }

        public ICollection<OrderItem> Products { get; set; }

        public ReceivingAddress ReceivingAddress { get; set; }
        public long UId { get; set; }

        public string NickName { get; set; }

        public string Account { get; set; }

        public string Email { get; set; }

        public long StoreId { get; set; }

        public string StoreName { get; set; }

        public string StoreLogoUri { get; set; }
    }
}
