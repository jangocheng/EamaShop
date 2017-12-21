﻿using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata;
using EamaShop.Infrastructures.Enums;

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
            //var status = apply.Property(x => x.AuditStatus)
            //    .HasDefaultValue()
            //    .ValueGeneratedOnAdd()
            //    .Metadata.BeforeSaveBehavior = PropertySaveBehavior.Ignore;
            apply.Property(x => x.StoreName).IsRequired();
            apply.Property(x => x.StoreLogoUri).IsRequired();
            apply.Property(x => x.StoreDescription).IsRequired();


            var store = modelBuilder.Entity<Store>();


        }
    }
}
