using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EamaShop.Identity.DataModel;
using Microsoft.EntityFrameworkCore;

namespace EamaShop.Identity.Services
{
    public class LoginService : ILoginService
    {
        private readonly DbContext _context;
        private readonly IPasswordFactory _passwordFactory;
        public LoginService(DbContext context, IPasswordFactory passwordFactory)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _passwordFactory = passwordFactory ?? throw new ArgumentNullException(nameof(passwordFactory));
        }
        public async Task<ApplicationUser> LoginAsync(string name, string password)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (password == null)
            {
                throw new ArgumentNullException(nameof(password));
            }

            var table = _context.Set<ApplicationUser>();

            var user = await table.FirstOrDefaultAsync(x => x.AccountName == name || x.Email == name || x.Phone == name);
            if (user == null)
            {
                throw new DomainException("用户不存在");
            }
            var result = _passwordFactory.FromSource(password, user.Salt);

            if (user.Password != result)
            {
                throw new DomainException("密码错误");
            }

            return user;
        }
    }
}
