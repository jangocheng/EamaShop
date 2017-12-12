using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EamaShop.Identity.DataModel;
using Microsoft.EntityFrameworkCore;
using EamaShop.Identity.Services.Respository.EntityConfig;

namespace EamaShop.Identity.Services.Respository
{
    public class UserContext : DbContext, IUserRespository, IUnitOfWork
    {

        public DbSet<ApplicationUser> User { get; set; }
        public IUnitOfWork UnitOfWork => this;

        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {

        }
        public Task<ApplicationUser> FindByIdentifier(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            return User.FirstOrDefaultAsync(x => x.AccountName == name || x.Email == name || x.Phone == name);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfig());
        }

        public void UpdateUser(ApplicationUser user)
        {
            Update(user);
        }

        public async Task<int> SaveEntitiesAsync()
        {
            return await SaveChangesAsync();
        }
    }
}
