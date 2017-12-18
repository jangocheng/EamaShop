using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata;
namespace EamaShop.Merchant.API.Infrastructures
{
    public class MerchantContext : DbContext
    {
        public MerchantContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Store> Store { get; set; }
        public DbSet<StoreCreateApply> StoreCreateApply { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var apply = modelBuilder.Entity<StoreCreateApply>();
            var createTime = apply.Property(x => x.CreateTime);
            createTime.HasDefaultValueSql("GETDATE()")
                .ValueGeneratedOnAdd();
            createTime.Metadata.AfterSaveBehavior = PropertySaveBehavior.Throw;
            createTime.Metadata.BeforeSaveBehavior = PropertySaveBehavior.Ignore;
            var status = apply.Property(x => x.AuditStatus)
                .HasDefaultValue(AuditStatus.Waiting)
                .ValueGeneratedOnAdd()
                .Metadata.BeforeSaveBehavior = PropertySaveBehavior.Ignore;
            apply.Property(x => x.Name).IsRequired();
            apply.Property(x => x.LogoUri).IsRequired();
            apply.Property(x => x.IsCreate).HasDefaultValue(true)
                .ValueGeneratedOnAdd()
                .Metadata.AfterSaveBehavior = PropertySaveBehavior.Save;
            apply.Property(x => x.Description).IsRequired();


            var store = modelBuilder.Entity<Store>();


        }
    }
}
