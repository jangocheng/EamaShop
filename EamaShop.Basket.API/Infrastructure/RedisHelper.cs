using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EamaShop.Basket.API.Infrastructure
{
    public class RedisHelper
    {
        private volatile ConnectionMultiplexer _connection;
        private readonly SemaphoreSlim _waitLock = new SemaphoreSlim(1, 1);
        private readonly string _connectionString;
        private IDatabase _database;
        public RedisHelper(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }
        public void SetAdd(string key, byte[] value)
        {
            Connect();

            _database.SetAdd(key, value);
        }
        public long SetCount(string key)
        {
            Connect();

            return _database.SetLength(key);
        }

        public void SetClear(string key)
        {
            Connect();

            _database.SetRemove(key, _database.SetMembers(key));
        }

        public bool SetContains(string key, byte[] value)
        {
            Connect();

            return _database.SetContains(key, value);
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

                _connection = ConnectionMultiplexer.Connect(_connectionString);
                _database = _connection.GetDatabase();
            }
            finally
            {
                _waitLock.Release();
            }
        }
    }
}
