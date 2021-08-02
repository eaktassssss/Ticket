
using Microsoft.Extensions.DependencyInjection;
namespace Ticket.Extensions.Redis
{
    public static class RedisConnectExtension
    {
        public static void AddRedisDistributedCacheExtension(this IServiceCollection services,string serverName)
        {
            services.AddDistributedRedisCache(option => {

                option.Configuration = serverName;
            });
        }
    }
}
