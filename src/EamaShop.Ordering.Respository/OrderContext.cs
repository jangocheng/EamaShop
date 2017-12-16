using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;

namespace EamaShop.Ordering.Respository
{
    public class OrderContext : DbContext
    {
        public OrderContext( DbContextOptions options) : base(options)
        {
        }
    }
}
