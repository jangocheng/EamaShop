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

            builder.Property(x => x.Description)
                .HasMaxLength(1600)
                .HasDefaultValue(string.Empty)
                .ValueGeneratedOnAddOrUpdate()
                .Metadata
                .BeforeSaveBehavior = PropertySaveBehavior.Save;

            builder.Property(x => x.Description)
                .Metadata
                .AfterSaveBehavior = PropertySaveBehavior.Save;

            var create = builder.Property(x => x.CreateTime)
                 .HasDefaultValueSql("CURRENT_TIMESTAMP")
                 .ValueGeneratedOnAdd()
                 .HasColumnType("TIMESTAMP");
            create.Metadata
                 .BeforeSaveBehavior = PropertySaveBehavior.Ignore;
            create.Metadata
                .AfterSaveBehavior = PropertySaveBehavior.Ignore;

            var modified = builder.Property(x => x.ModifiedTime)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnAddOrUpdate()
                .HasColumnType("TIMESTAMP");
            modified.Metadata
                .BeforeSaveBehavior = PropertySaveBehavior.Ignore;
            modified.Metadata
                .AfterSaveBehavior = PropertySaveBehavior.Ignore;


        }
    }
}
