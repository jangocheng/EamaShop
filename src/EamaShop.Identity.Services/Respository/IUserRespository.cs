using EamaShop.Identity.DataModel;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EamaShop.Identity.Services.Respository
{
    public interface IUserRespository
    {
        IUnitOfWork UnitOfWork { get; }
        /// <summary>
        /// 通过能确定用户唯一标识的名称查找用户  手机/用户名/邮箱
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<ApplicationUser> FindByIdentifier(string name);

        void UpdateUser(ApplicationUser user);

        Task<bool> Contains(Expression<Func<ApplicationUser, bool>> predicate, 
            CancellationToken cancellationToken = default(CancellationToken));

        Task<ApplicationUser> AddAsync(ApplicationUser user,
             CancellationToken cancellationToken = default(CancellationToken));
    }
}
