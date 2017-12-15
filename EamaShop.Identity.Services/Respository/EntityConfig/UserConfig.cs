using EamaShop.Identity.DataModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata;
using EamaShop.Infrastructures;

namespace EamaShop.Identity.Services.Respository.EntityConfig
{
    public class UserConfig : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.HasIndex(x => x.AccountName);
            builder.HasIndex(x => x.Phone);
            builder.HasIndex(x => x.Email);

            builder.ToTable("User");

            builder.Property(x => x.AccountName).IsRequired();

            builder.Property(x => x.Phone)
                .IsRequired()
                .HasDefaultValue(string.Empty)
                .ValueGeneratedOnAddOrUpdate()
                .Metadata
                .BeforeSaveBehavior = PropertySaveBehavior.Save;

            builder.Property(x => x.Email)
                .IsRequired()
                .HasDefaultValue(string.Empty)
                .ValueGeneratedOnAddOrUpdate()
                .Metadata
                .BeforeSaveBehavior = PropertySaveBehavior.Save;

            builder.Property(x => x.HeadImageUri)
                .IsRequired();

            builder.Property(x => x.CreateTime)
                .IsRequired()
                .HasDefaultValueSql("now()")
                .ValueGeneratedOnAdd()
                .Metadata
                .AfterSaveBehavior = PropertySaveBehavior.Ignore;
            builder.Property(x => x.CreateTime)
                .Metadata
                .BeforeSaveBehavior = PropertySaveBehavior.Ignore;


            builder.Property(x => x.Password).IsRequired();

            builder.Property(x => x.Sexy)
                .IsRequired()
                .HasDefaultValue(UserGender.Male);

            builder.Property(x => x.Salt).IsRequired();
        }
    }
}
