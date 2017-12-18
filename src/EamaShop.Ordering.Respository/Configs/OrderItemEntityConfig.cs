using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EamaShop.Ordering.Respository.Configs
{
    public class OrderItemEntityConfig : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.OrderId);
            builder.HasIndex(x => x.OrderNumber);

            builder.Property(x => x.OrderNumber).IsRequired();
            builder.Property(x => x.ProductName).IsRequired();
            builder.Property(x => x.ProductPicture).IsRequired();
            builder.Property(x => x.SpecificationName).IsRequired();
        }
    }
}
