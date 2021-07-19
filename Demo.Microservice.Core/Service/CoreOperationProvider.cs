using Microsoft.Extensions.DependencyInjection;
using System;

namespace Demo.Microservice.Core
{
    internal class CoreOperationProvider : ICoreOperationProvider
    {
        private readonly IServiceProvider _serviceProvider;

        public CoreOperationProvider(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public TCoreOperation GetCoreOperation<TCoreOperation>()
        {
            return _serviceProvider.GetRequiredService<TCoreOperation>();
        }
    }
}
