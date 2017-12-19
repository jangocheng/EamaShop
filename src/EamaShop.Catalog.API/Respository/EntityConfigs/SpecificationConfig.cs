using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata;
using EamaShop.Catalog.API.Infrastructures;

namespace EamaShop.Catalog.API.Respository.EntityConfigs
{
    public class SpecificationConfig : IEntityTypeConfiguration<Specification>
    {
        public void Configure(EntityTypeBuilder<Specification> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.ProductId);
            builder.HasIndex(x => x.Price);


            builder.Property(x => x.Name).IsRequired();

            builder.Property(x => x.StockCount).HasDefaultValue(99999)
                .ValueGeneratedOnAdd()
                .Metadata.BeforeSaveBehavior = PropertySaveBehavior.Save;

            var create = builder.Property(x => x.CreateTime)
                 .HasDefaultValueSql("CURRENT_TIMESTAMP")
                 .HasColumnType("TIMESTAMP")
                 .ValueGeneratedOnAdd();
            create.Metadata
            .BeforeSaveBehavior = PropertySaveBehavior.Ignore;
            create.Metadata
                .AfterSaveBehavior = PropertySaveBehavior.Throw;

            var modified = builder.Property(x => x.ModifiedTime)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("TIMESTAMP")
                .ValueGeneratedOnAddOrUpdate();
            modified.Metadata
                .AfterSaveBehavior = PropertySaveBehavior.Ignore;
            modified.Metadata
                .BeforeSaveBehavior = PropertySaveBehavior.Ignore;

            builder.Property(x => x.State)
                .HasDefaultValue(SpecificationState.OnSell)
                .ValueGeneratedOnAdd()
                .Metadata.BeforeSaveBehavior = PropertySaveBehavior.Save;
        }
    }
}
