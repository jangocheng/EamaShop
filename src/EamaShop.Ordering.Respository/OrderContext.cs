using EamaShop.Ordering.Respository.Configs;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;

namespace EamaShop.Ordering.Respository
{
    public class OrderContext : DbContext
    {
        public OrderContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Order> Order { get; set; }

        public DbSet<OrderItem> OrderItem { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new OrderEntityConfig());
            modelBuilder.ApplyConfiguration(new ReceivingAddressEntityConfig());
        }
    }
}
