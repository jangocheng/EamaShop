using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EamaShop.Infrastructures
{
    public class RedisLock : IDistributedLock, IEquatable<RedisLock>
    {
        private readonly IDatabase _database;
        private string TOKEN = "redis_locked_token";
        public RedisLock(IDatabase database, string name)
        {
            _database = database ?? throw new ArgumentNullException(nameof(database));
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public string Name { get; }

        public bool Enter(TimeSpan expired)
        {
            return _database.LockTake(Name, TOKEN, expired);
        }

        public Task<bool> EnterAsync(TimeSpan expired, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            return _database.LockTakeAsync(Name, TOKEN, expired);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as RedisLock);
        }

        public bool Equals(RedisLock other)
        {
            return other != null &&
                   TOKEN == other.TOKEN &&
                   Name == other.Name;
        }

        public void Exit()
        {
            _database.LockRelease(Name, TOKEN);
        }

        public Task ExitAsync()
        {
            return _database.LockReleaseAsync(Name, TOKEN);
        }

        public override int GetHashCode()
        {
            var hashCode = -1949558680;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(TOKEN);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            return hashCode;
        }

        public static bool operator ==(RedisLock lock1, RedisLock lock2)
        {
            return EqualityComparer<RedisLock>.Default.Equals(lock1, lock2);
        }

        public static bool operator !=(RedisLock lock1, RedisLock lock2)
        {
            return !(lock1 == lock2);
        }
    }
}