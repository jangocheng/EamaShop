using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EamaShop.Merchant.API.Infrastructures
{
    public class MerchantContext : DbContext
    {
        public MerchantContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Store> Store { get; set; }
    }
}
