using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EamaShop.Infrastructures
{
    /// <summary>
    /// 表示分布式锁对象
    /// </summary>
    public interface IDistributedLock
    {
        /// <summary>
        /// 获取该锁的唯一名称
        /// </summary>
        string Name { get; }
        /// <summary>
        /// 尝试获取独占锁
        /// </summary>
        /// <param name="expired">独占该锁的最长时间</param>
        /// <returns>如果能独占成功，返回true，否则，则认为该锁已经被其他对象独占，此时返回false</returns>
        bool Enter(TimeSpan expired);
        /// <summary>
        /// 尝试异步的方式获取独占锁
        /// </summary>
        /// <param name="expired">独占该锁的最长时间</param>
        /// <param name="cancellationToken">用于取消任务的信号标</param>
        /// <returns>如果能独占成功，返回true，否则，则认为该锁已经被其他对象独占，此时返回false</returns>
        Task<bool> EnterAsync(TimeSpan expired, CancellationToken cancellationToken = default(CancellationToken));
        /// <summary>
        /// 归还独占锁的占用
        /// </summary>
        void Exit();
        /// <summary>
        /// 归还独占锁的占用
        /// </summary>
        /// <returns></returns>
        Task ExitAsync();
    }
}