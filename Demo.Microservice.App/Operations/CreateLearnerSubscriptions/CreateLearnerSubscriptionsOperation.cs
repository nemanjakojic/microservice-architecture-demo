using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Demo.Microservice.Core;
using Demo.Microservice.App.Data.Context;
using Demo.Microservice.Core.Service;
using Demo.Microservice.App.Data.Entity;
using Demo.Microservice.Core.Extensions;

namespace Demo.Microservice.App.Operations.CreateLearnerSubscriptions
{
    public class CreateLearnerSubscriptionsOperation : CoreOperationBase<CreateLearnerSubscriptionsRequest, CreateLearnerSubscriptionResponse>
    {
        private readonly ISubscriptionDbContext _context;
        private readonly IDateTimeService _dateTimeService;

        public CreateLearnerSubscriptionsOperation(ISubscriptionDbContext dbContext, IDateTimeService dateTimeService, ILogger<CreateLearnerSubscriptionsOperation> logger) : base(logger)
        {
            _context = dbContext;
            _dateTimeService = dateTimeService;
        }

        protected override async Task<ValidationResult> ValidateRequest(CreateLearnerSubscriptionsRequest request, CancellationToken cancellationToken)
        {
            var defaultCheck = await base.ValidateRequest(request, cancellationToken);
            if (!defaultCheck.Valid)
            {
                return defaultCheck;
            }

            if (request.InstitutionSubscriptionData == null)
            {
                return ValidationResult.Failure().WithMessage("Institution subscription data is null.");
            }

            if (request.Learners == null || !request.Learners.Any())
            {
                return ValidationResult.Failure().WithMessage("No learners were specified.");
            }

            return ValidationResult.Success();
        }

        protected override async Task<CreateLearnerSubscriptionResponse> ExecuteRequest(CreateLearnerSubscriptionsRequest request, CancellationToken cancellationToken)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

            var newLearnerSubscriptions = await PrepareNewLearnerSubscriptions(request);
            foreach (var newSubsiption in newLearnerSubscriptions)
            {
                await _context.MemberSubscription.AddAsync(newSubsiption);
            }

            await _context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);

            return new CreateLearnerSubscriptionResponse
            {
                CreatedSubscriptions = newLearnerSubscriptions.Select(s => new CreateLearnerSubscriptionResponse.LearnerSubscription
                {
                    LearnerAccountId = s.AccountID,
                    LearnerSubscriptionId = s.ID
                })
            }.Success();
        }

        private async Task<IEnumerable<MemberSubscription>> PrepareNewLearnerSubscriptions(CreateLearnerSubscriptionsRequest request)
        {
            DateTime now = _dateTimeService.UtcNow();

            var institutionSubscription = request.InstitutionSubscriptionData;
            var subscriptionValidityPeriod = (institutionSubscription.EndDate - institutionSubscription.StartDate);

            var unusedExpirationDays = await GetUnusedExpirationDays(institutionSubscription.ExamBankId);

            List<MemberSubscription> subscriptions = new List<MemberSubscription>();
            foreach (var learner in request.Learners)
            {
                var examBank = _context.ExamBank.Single(eb => eb.ID == request.InstitutionSubscriptionData.ExamBankId);
                var examYear = _context.ExamYear.Single(ay => ay.ID == request.InstitutionSubscriptionData.ExamYearId);
                var instituionSubscription = _context.InstitutionSubscription.Single(s => s.InstitutionSubscriptionId == request.InstitutionSubscriptionData.InstitutionSubscriptionId);

                MemberSubscription newSubscription = new MemberSubscription
                {
                    AccountID = learner.AccountId,
                    ExternalId = Guid.NewGuid(),
                    // InstitutionSubscriptionId = request.InstitutionSubscriptionData.InstitutionSubscriptionId,
                    InstitutionSubscription = instituionSubscription,
                    ExamBank = examBank,
                    ExamYear = examYear,
                    GradYear = learner.GraduationYear,
                    Active = true,
                    AvailableDate = now,
                    ValidityStartDate = request.InstitutionSubscriptionData.StartDate,
                    ValidityPeriod = subscriptionValidityPeriod.Days,
                    AvailablePeriod = unusedExpirationDays,
                    TimeStamp = now
                };
                subscriptions.Add(newSubscription);
            }
            return subscriptions;
        }

        private Task<int> GetUnusedExpirationDays(int examBankId)
        {
            return Task.FromResult(90); // TODO: implement this
        }
    }
}
