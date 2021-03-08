using Microsoft.Extensions.Configuration;
using RouletteBetting.Models.Abstract;
using ServiceStack.Redis;
using System;

namespace RouletteBetting.Models.Repository
{
    public class RedisCacheRepository:IRedisCache
    {
        private IConfiguration _configuration;
        private readonly RedisEndpoint _redisConfiguration;

        public RedisCacheRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _redisConfiguration = new RedisEndpoint() { Host = _configuration["RedisConfig:Host"], Password = _configuration["RedisConfig:Password"], Port = Convert.ToInt32(_configuration["RedisConfig:Port"]),Ssl = Convert.ToBoolean(_configuration["RedisConfig:Ssl"])};
        }

        public void Set<T>(string key, T value, TimeSpan time) where T : class
        {
            using (IRedisClient client = new RedisClient(_redisConfiguration))
            {
                client.Set(key, value, time);
            }
        }
        public T Get<T>(string key) where T : class
        {
            using (IRedisClient client = new RedisClient(_redisConfiguration))
            {
                return client.Get<T>(key);
            }
        }
    }
}
