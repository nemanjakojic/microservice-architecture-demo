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
using static Demo.Microservice.App.Operations.GetLearnerSubscriptions.GetLearnerSubscriptionsResponse;

namespace Demo.Microservice.App.Operations.GetLearnerSubscriptions
{
    public class GetLearnerSubscriptionsOperation : CoreOperationBase<GetLearnerSubscriptionsRequest, GetLearnerSubscriptionsResponse>
    {
        private readonly ISubscriptionDbContext _context;
        private readonly IDateTimeService _dateTimeService;

        public GetLearnerSubscriptionsOperation(ISubscriptionDbContext context, IDateTimeService dateTimeService, ILogger<GetLearnerSubscriptionsOperation> logger)
            : base(logger)
        {
            _context = context;
            _dateTimeService = dateTimeService;
        }

        protected override Task<ValidationResult> ValidateRequest(GetLearnerSubscriptionsRequest request)
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

        protected async override Task<GetLearnerSubscriptionsResponse> ExecuteRequest(GetLearnerSubscriptionsRequest request, ValidationResult validation)
        {
            var subscriptions = await GetMemberSubscriptions(request);
            return new GetLearnerSubscriptionsResponse { SearchResult = subscriptions }.Success();
        }
        
        private async Task<PagedResult<IEnumerable<MemberSubscription>>> GetMemberSubscriptions(GetLearnerSubscriptionsRequest request)
        {
            var utcNow = _dateTimeService.UtcNow();

            var query = from ms in _context.MemberSubscription
                            .Include(s => s.ExamBank)
                            .Include(s => s.ExamYear)
                            .Include(s => s.InstitutionSubscription)
                        where
                            ms.AccountID == request.AccountId
                            && ms.InstitutionSubscription.InstitutionNodeId == request.InstitutionId
                            && ms.ValidityStartDate != null
                            && ms.ValidityPeriod != null
                            && utcNow < ms.ValidityStartDate.Value.AddDays(ms.ValidityPeriod.Value)
                        select
                            new MemberSubscription
                            {
                                ID = ms.ID,
                                ExamBankName = ms.ExamBank.Name,
                                ExamYearName = ms.ExamYear.Name,
                                ValidityStartDate = ms.ValidityStartDate.Value,
                                ValidityEndDate = ms.ValidityStartDate.Value.AddDays(ms.ValidityPeriod.Value),
                                AccountID = ms.AccountID,
                                Active = ms.Active,
                                ExamBankID = ms.ExamBank.ID,
                                ExamYearID = ms.ExamYear.ID,
                                GraduationYear = ms.GradYear ?? 0,
                                InstitutionSubscriptionId = ms.InstitutionSubscription.InstitutionSubscriptionId,
                                ValidityPeriod = ms.ValidityPeriod,
                                LastLogin = ms.LastLogin
                            };
            
            var subscriptions = await query.ToListAsync();
            var totalCount = subscriptions.Count();
            
            // Normalize exam year names (drop prefix AY = Academic Year)
            // Note: the normalization cannot be translated to SQL by LINQ
            subscriptions.ForEach(s => s.ExamYearName = AppUtils.NormalizeExamYearName(s.ExamYearName));

            var resultPage = subscriptions.AsQueryable().SortAndPage(request.SubscriptionFilter).ToList();
            return new PagedResult<IEnumerable<MemberSubscription>>
            {
                Total = totalCount,
                Items = resultPage
            };
        }
    }
}
