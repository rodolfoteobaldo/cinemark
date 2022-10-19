using StackExchange.Redis;
using StackExchange.Redis.Extensions.Core.Abstractions;

namespace CinemarkTest.Infra.Redis;

public static class RedisDatabaseExtension
{
    public static async Task<T> GetOrSetAsync<T>(this IRedisDatabase redisDatabase, string key,
        Func<Task<T>> valueFactory, TimeSpan expiration)
    {
        var result = await redisDatabase.GetAsync<T>(key);
        if (result != null) return result;
        var value = await valueFactory();
        if (value != null)
            await redisDatabase.AddAsync(key, value, expiration, When.Always, CommandFlags.FireAndForget);
        return value;
    }
}