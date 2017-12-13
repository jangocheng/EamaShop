using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EamaShop.Catalog.API.Respository.EntityConfigs
{
    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        void IEntityTypeConfiguration<Product>.Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.CategoryId);
            builder.HasIndex(x => x.Name);
            builder.HasIndex(x => x.StoreId);

            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.PictureUris).IsRequired();
            builder.Property(x => x.Properties).IsRequired();
            builder.Property(x => x.Specifications).IsRequired();

            builder.Property(x => x.Description)
                .HasDefaultValue(string.Empty)
                .ValueGeneratedOnAddOrUpdate()
                .Metadata
                .BeforeSaveBehavior = PropertySaveBehavior.Save;

            builder.Property(x => x.Description)
                .Metadata
                .AfterSaveBehavior = PropertySaveBehavior.Save;

        }
    }
}
