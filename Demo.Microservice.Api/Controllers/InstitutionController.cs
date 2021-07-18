using Demo.Microservice.App.Operations.CreateLearnerSubscriptions;
using Demo.Microservice.App.Operations.GetLearnerSubscriptions;
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
    public class InstitutionController : ControllerBase
    {
        private readonly ICoreOperationProvider _coreOperationProvider;

        public InstitutionController(ICoreOperationProvider coreServiceFactory)
        {
            _coreOperationProvider = coreServiceFactory;
        }

        [HttpPost]
        [Route("{institutionId}/account/{accountId}/subscriptions")]
        public async Task<ActionResult<GetLearnerSubscriptionsResponse>> GetFilteredMemberSubscriptions([FromRoute] Guid institutionId, [FromRoute] int accountId, [FromBody] QueryFilter queryFilter)
        {
            var request = new GetLearnerSubscriptionsRequest
            {
                InstitutionId = institutionId,
                AccountId = accountId,
                SubscriptionFilter = queryFilter
            };

            var appOperation = _coreOperationProvider.GetCoreOperation<GetLearnerSubscriptionsOperation>();
            return await appOperation.Execute(request);
        }

        [HttpPost("{institutionId}/subscriptions/create")]
        public async Task<ActionResult<CreateLearnerSubscriptionResponse>> CreateLearnerSubscriptions([FromRoute] Guid institutionId, [FromBody] CreateLearnerSubscriptionsRequest request)
        {
            var appOperation = _coreOperationProvider.GetCoreOperation<CreateLearnerSubscriptionsOperation>();
            return await appOperation.Execute(request);
        }
    }
}
