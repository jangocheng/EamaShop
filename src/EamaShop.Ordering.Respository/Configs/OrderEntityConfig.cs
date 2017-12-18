using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EamaShop.Ordering.Respository.Configs
{
    public class OrderEntityConfig : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.OrderNumber);
            builder.HasIndex(x => x.UId);

            builder.OwnsOne(x => x.ReceivingAddress);
            
        }
    }
}
