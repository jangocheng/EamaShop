using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EamaShop.Infrastructures
{
    /// <summary>
    /// 表示用于获取分布式锁的提供程序
    /// </summary>
    public interface IDistributedLockProvider
    {
        /// <summary>
        /// 创建或获取
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IDistributedLock GetLock(string name);
        Task<IDistributedLock> GetLockAsync(string name, CancellationToken cancellationToken = default(CancellationToken));
    }
}
