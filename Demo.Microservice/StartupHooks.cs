using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Demo.Microservice.App.Data.Context;
using Demo.Microservice.App.Operations.CreateSubscriptions;
using Demo.Microservice.App.Operations.GetSubscriptions;

namespace Demo.Microservice.App
{
    public static class StartupHooks
    {
        public static void ConfigureServices(IServiceCollection services, DbContextOptions dbContextOptions)
        {
            Demo.Microservice.Core.StartupHooks.ConfigureServices(services);

            services.AddTransient<GetSubscriptionOperation>();
            services.AddTransient<CreateSubscriptionOperation>();
            services.AddTransient<ISubscriptionDbContext>(x => new SubscriptionDbContext(dbContextOptions));
        }
    }
}
