using EamaShop.Identity.DataModel;
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
    }
}
