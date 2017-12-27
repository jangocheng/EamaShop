using EamaShop.Identity.DataModel;
using EamaShop.Infrastructures.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EamaShop.Identity.Services
{
    /// <summary>
    /// Provides methods for modified user's informations.
    /// </summary>
    public interface IUserInfoService
    {
        /// <summary>
        /// Modified user inforamtions.
        /// </summary>
        /// <param name="configure"></param>
        /// <returns></returns>
        /// <exception cref="DomainException">用户不存在</exception>
        /// <exception cref="ArgumentNullException"><paramref name="configure"/>参数不能为null</exception>
        Task EditInfo(long id, Action<UserEditor> configure,
            CancellationToken cancellationToken = default(CancellationToken));
        /// <summary>
        /// 设置新密码
        /// </summary>
        /// <param name="id"></param>
        /// <param name="password"></param>
        /// <param name="token"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task ChangePasswordAsync(long id, string password,
            string token,
            CancellationToken cancellationToken = default(CancellationToken));

        Task BindPhone(long id, string phone, string verifyCode,
            CancellationToken cancellationToken = default(CancellationToken));

        Task<ApplicationUser> GetByIdAsync(long id, CancellationToken cancellationToken = default(CancellationToken));


        /// <summary>
        /// Get User By phone number.
        /// </summary>
        /// <returns></returns>
        Task<ApplicationUser> GetByPhoneAsync(string phone,
            CancellationToken cancellationToken = default(CancellationToken));

        Task<ApplicationUser> GetByEmailAsync(string email,
            CancellationToken cancellationToken = default(CancellationToken));
        Task ChangeRole(long id, UserRole role);
    }
}
