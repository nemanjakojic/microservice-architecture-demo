using Demo.Microservice.App.Common.Util;
using Demo.Microservice.App.Data.Context;
using Demo.Microservice.Core;
using Demo.Microservice.Core.Common.Model;
using Demo.Microservice.Core.Extensions;
using Demo.Microservice.Core.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static Demo.Microservice.App.Operations.GetSubscriptions.GetSubscriptionsResponse;

namespace Demo.Microservice.App.Operations.GetSubscriptions
{
    public class GetSubscriptionsOperation : CoreOperationBase<GetSubscriptionsRequest, GetSubscriptionsResponse>
    {
        private readonly ISubscriptionDbContext _context;
        private readonly IDateTimeService _dateTimeService;

        public GetSubscriptionsOperation(ISubscriptionDbContext context, IDateTimeService dateTimeService, ILogger<GetSubscriptionsOperation> logger)
            : base(logger)
        {
            _context = context;
            _dateTimeService = dateTimeService;
        }

        protected override Task<ValidationResult> ValidateRequest(GetSubscriptionsRequest request)
        {
            if (request.InstitutionId == Guid.Empty)
            {
                return ValidationResult.Failure().WithError($"Invalid institution id: { request.InstitutionId }.").ToTask();
            }

            if (request.AccountId <= 0)
            {
                return ValidationResult.Failure().WithError($"Invalid account id: { request.AccountId }.").ToTask();
            }

            return ValidationResult.Success().ToTask();
        }

        protected async override Task<GetSubscriptionsResponse> ExecuteRequest(GetSubscriptionsRequest request, ValidationResult validation)
        {
            var subscriptions = await GetMemberSubscriptions(request);
            return new GetSubscriptionsResponse { SearchResult = subscriptions }.Success();
        }
        
        private async Task<PagedResult<IEnumerable<StudentSubscription>>> GetMemberSubscriptions(GetSubscriptionsRequest request)
        {
            var utcNow = _dateTimeService.UtcNow();

            var query = from ms in _context.StudentSubscription
                            .Include(s => s.QuestionBank)
                            .Include(s => s.InstitutionSubscription)
                        where
                            ms.AccountID == request.AccountId
                            && ms.InstitutionSubscription.InstitutionId == request.InstitutionId
                            && ms.ValidityStartDate != null
                            && ms.ValidityPeriod != null
                            && utcNow < ms.ValidityStartDate.Value.AddDays(ms.ValidityPeriod.Value)
                        select
                            new StudentSubscription
                            {
                                ID = ms.ID,
                                QuestionBankName = ms.QuestionBank.Name,
                                ValidityStartDate = ms.ValidityStartDate.Value,
                                ValidityEndDate = ms.ValidityStartDate.Value.AddDays(ms.ValidityPeriod.Value),
                                AccountID = ms.AccountID,
                                Active = ms.Active,
                                QuestionBankID = ms.QuestionBank.ID,
                                GraduationYear = ms.GradYear ?? 0,
                                InstitutionSubscriptionId = ms.InstitutionSubscription.Id
                            };
            
            var subscriptions = await query.ToListAsync();
            var totalCount = subscriptions.Count();
            
            var resultPage = subscriptions.AsQueryable().SortAndPage(request.SubscriptionFilter).ToList();
            return new PagedResult<IEnumerable<StudentSubscription>>
            {
                Total = totalCount,
                Items = resultPage
            };
        }
    }
}
