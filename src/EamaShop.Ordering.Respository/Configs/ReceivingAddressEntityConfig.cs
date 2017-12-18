using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EamaShop.Ordering.Respository.Configs
{
    public class ReceivingAddressEntityConfig : IEntityTypeConfiguration<ReceivingAddress>
    {
        public void Configure(EntityTypeBuilder<ReceivingAddress> builder)
        {
            builder.HasIndex(x => x.Receiver);
            builder.HasIndex(x => x.ContactPhone);
        }
    }
}
