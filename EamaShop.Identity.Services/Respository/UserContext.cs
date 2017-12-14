using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EamaShop.Identity.DataModel;
using Microsoft.EntityFrameworkCore;
using EamaShop.Identity.Services.Respository.EntityConfig;
using System.Linq.Expressions;
using System.Threading;

namespace EamaShop.Identity.Services.Respository
{
    public class UserContext : DbContext, IUserRespository, IUnitOfWork
    {

        public DbSet<ApplicationUser> User { get; set; }

        IUnitOfWork IUserRespository.UnitOfWork => this;

        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfig());
        }



        public async Task<int> SaveEntitiesAsync()
        {
            return await SaveChangesAsync();
        }

        Task<ApplicationUser> IUserRespository.FindByIdentifier(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            return User.FirstOrDefaultAsync(x => x.AccountName == name || x.Email == name || x.Phone == name);
        }

        void IUserRespository.UpdateUser(ApplicationUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            Update(user);
        }

        Task<bool> IUserRespository.Contains(Expression<Func<ApplicationUser, bool>> predicate, CancellationToken cancellationToken)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            cancellationToken.ThrowIfCancellationRequested();

            return User.AnyAsync(predicate, cancellationToken);
        }

        async Task<ApplicationUser> IUserRespository.AddAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            cancellationToken.ThrowIfCancellationRequested();

            var entry = await User.AddAsync(user, cancellationToken);
            
            return entry.Entity;
        }
    }
}
