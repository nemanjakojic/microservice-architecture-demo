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

namespace Demo.Microservice.App.Operations.CreateSubscriptions
{
    public class CreateSubscriptionsOperation : CoreOperationBase<CreateSubscriptionsRequest, CreateSubscriptionResponse>
    {
        private readonly ISubscriptionDbContext _context;
        private readonly IDateTimeService _dateTimeService;

        public CreateSubscriptionsOperation(ISubscriptionDbContext dbContext, IDateTimeService dateTimeService, ILogger<CreateSubscriptionsOperation> logger) : base(logger)
        {
            _context = dbContext;
            _dateTimeService = dateTimeService;
        }

        protected override Task<ValidationResult> ValidateRequest(CreateSubscriptionsRequest request)
        {
            if (request.InstitutionSubscriptionData == null)
            {
                return ValidationResult.Failure().WithError("Institution subscription data is null.").ToTask();
            }

            if (request.Students == null || !request.Students.Any())
            {
                return ValidationResult.Failure().WithError("No learners were specified.").ToTask();
            }

            return ValidationResult.Success().ToTask();
        }

        protected override async Task<CreateSubscriptionResponse> ExecuteRequest(CreateSubscriptionsRequest request, ValidationResult validation)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync();

            var newLearnerSubscriptions = await PrepareNewStudentSubscriptions(request);
            foreach (var newSubsiption in newLearnerSubscriptions)
            {
                await _context.StudentSubscription.AddAsync(newSubsiption);
            }

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();

            return new CreateSubscriptionResponse
            {
                CreatedSubscriptions = newLearnerSubscriptions.Select(s => new CreateSubscriptionResponse.StudentSubscription
                {
                    LearnerAccountId = s.AccountID,
                    LearnerSubscriptionId = s.ID
                })
            }.Success();
        }

        private async Task<IEnumerable<StudentSubscription>> PrepareNewStudentSubscriptions(CreateSubscriptionsRequest request)
        {
            DateTime now = _dateTimeService.UtcNow();

            var institutionSubscription = request.InstitutionSubscriptionData;
            var subscriptionValidityPeriod = (institutionSubscription.EndDate - institutionSubscription.StartDate);

            var unusedExpirationDays = await GetUnusedExpirationDays(institutionSubscription.QuestionBankId);

            List<StudentSubscription> subscriptions = new List<StudentSubscription>();
            foreach (var learner in request.Students)
            {
                var questionBank = _context.QuestionBank.Single(eb => eb.ID == request.InstitutionSubscriptionData.QuestionBankId);
                var instituionSubscription = _context.InstitutionSubscription.Single(s => s.Id == request.InstitutionSubscriptionData.Id);

                StudentSubscription newSubscription = new StudentSubscription
                {
                    AccountID = learner.AccountId,
                    InstitutionSubscription = instituionSubscription,
                    QuestionBank = questionBank,
                    GradYear = learner.GraduationYear,
                    Active = true,
                    ValidityStartDate = request.InstitutionSubscriptionData.StartDate,
                    ValidityPeriod = subscriptionValidityPeriod.Days,
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
