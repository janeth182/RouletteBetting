using System;

namespace RouletteBetting.Models.Abstract
{
    public interface IRedisCache
    {
        void Set<T>(string key, T value, TimeSpan time) where T : class;
        T Get<T>(string key) where T : class;        
    }
}
