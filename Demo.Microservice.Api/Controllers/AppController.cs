using Demo.Microservice.App.Operations.CreateSubscriptions;
using Demo.Microservice.App.Operations.GetSubscriptions;
using Demo.Microservice.Core;
using Demo.Microservice.Core.Common.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demo.Microservice.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppController : ControllerBase
    {
        private readonly ICoreOperationProvider _coreOperationProvider;

        public AppController(ICoreOperationProvider coreServiceFactory)
        {
            _coreOperationProvider = coreServiceFactory;
        }

        [HttpPost]
        [Route("{institutionId}/account/{accountId}/subscriptions")]
        public async Task<ActionResult<GetSubscriptionResponse>> GetSubscriptions([FromRoute] Guid institutionId, [FromRoute] int accountId, [FromBody] QueryFilter queryFilter)
        {
            var request = new GetSubscriptionRequest
            {
                InstitutionId = institutionId,
                AccountId = accountId,
                SubscriptionFilter = queryFilter
            };

            var appOperation = _coreOperationProvider.GetCoreOperation<GetSubscriptionOperation>();
            return await appOperation.Execute(request);
        }

        [HttpPost("{institutionId}/subscription")]
        public async Task<ActionResult<CreateSubscriptionResponse>> CreateSubscriptions([FromRoute] Guid institutionId, [FromBody] CreateSubscriptionRequest request)
        {
            var appOperation = _coreOperationProvider.GetCoreOperation<CreateSubscriptionOperation>();
            return await appOperation.Execute(request);
        }
    }
}
