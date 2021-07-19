using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Demo.Microservice.Core.Service;

namespace Demo.Microservice.Core
{
    public static class StartupHooks
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IDateTimeService, DateTimeService>();
            services.AddScoped<ICoreOperationProvider, CoreOperationProvider>();
        }
    }
}
