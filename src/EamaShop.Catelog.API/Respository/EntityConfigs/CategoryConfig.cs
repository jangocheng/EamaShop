using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EamaShop.Catalog.API.Respository.EntityConfigs
{
    public class CategoryConfig : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Name);
            builder.HasIndex(x => x.ParentId);
            builder.HasIndex(x => x.StoreId);

            builder.Property(x => x.Name).IsRequired();
            
        }
    }
}
