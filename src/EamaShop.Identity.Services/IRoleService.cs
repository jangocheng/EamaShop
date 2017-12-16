using EamaShop.Identity.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EamaShop.Identity.Services
{
    public interface IRoleService
    {
        /// <summary>
        /// 修改指定用户的身份角色
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        Task ChangeRole(long uid, params UserRole[] role);
    }
}
