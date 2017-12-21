using Microsoft.Extensions.Options;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EamaShop.Infrastructures
{
    public class RedisLockProvider : IDistributedLockProvider
    {
        private readonly RedisLockOptions _options;
        private readonly SemaphoreSlim _waitLock;
        private volatile ConnectionMultiplexer _connection;
        private IDatabase _database;
        public RedisLockProvider(IOptions<RedisLockOptions> options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }
            _options = options.Value;
            _waitLock = new SemaphoreSlim(1, 1);
        }
        public IDistributedLock GetLock(string name)
        {
            Connect();
            return new RedisLock(_database, name);
        }


        public async Task<IDistributedLock> GetLockAsync(string name, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();

            await ConnectAsync(cancellationToken);
            return new RedisLock(_database, name);
        }
        private void Connect()
        {
            if (_connection != null)
            {
                return;
            }
            _waitLock.Wait();
            try
            {
                if (_connection != null)
                {
                    return;
                }
                _connection = ConnectionMultiplexer.Connect(_options.Configuration);
                _database = _connection.GetDatabase();
            }
            finally
            {
                _waitLock.Release();
            }
        }
        private async Task ConnectAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (_connection != null)
            {
                return;
            }
            await _waitLock.WaitAsync(cancellationToken);
            try
            {
                if (_connection != null)
                {
                    return;
                }
                _connection = await ConnectionMultiplexer.ConnectAsync(_options.Configuration);
                _database = _connection.GetDatabase();
            }
            finally
            {
                _waitLock.Release();
            }
        }
    }
}
