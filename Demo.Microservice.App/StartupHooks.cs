using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Demo.Microservice.App.Data.Context;
using Demo.Microservice.App.Operations.CreateLearnerSubscriptions;
using Demo.Microservice.App.Operations.GetLearnerSubscriptions;

namespace Demo.Microservice.App
{
    public static class StartupHooks
    {
        public static void ConfigureServices(IServiceCollection services, DbContextOptions dbContextOptions)
        {
            Demo.Microservice.Core.StartupHooks.ConfigureServices(services);

            services.AddTransient<GetLearnerSubscriptionsOperation>();
            services.AddTransient<CreateLearnerSubscriptionsOperation>();
            services.AddTransient<ISubscriptionDbContext>(x => new SubscriptionDbContext(dbContextOptions));
        }
    }
}
