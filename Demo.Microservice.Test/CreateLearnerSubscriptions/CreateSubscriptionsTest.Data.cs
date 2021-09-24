using Demo.Microservice.App.Data.Context;
using Demo.Microservice.App.Data.Entity;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Demo.Microservice.App.Test.CreateLearnerSubscriptions
{
    public partial class CreateSubscriptionsTest
    {
        private static readonly Guid ValidInstitutionId = Guid.Parse("A1A1A1A1-A1A1-A1A1-A1A1-A1A1A1A1A1A1");
        private static readonly Guid ValidInstitutionSubscriptionId = Guid.Parse("B1B1B1B1-B1B1-B1B1-B1B1-B1B1B1B1B1B1");
        private const int ValidAccountId = 111111;

        private SubscriptionDbContext InitDataModel()
        {
            var dbContext = ServiceProvider.GetRequiredService<SubscriptionDbContext>();
            InitInstitutionSubscriptions(dbContext);
            InitQuestionBanks(dbContext);
            InitStudentSubscriptions(dbContext);
            return dbContext;
        }

        private void InitStudentSubscriptions(SubscriptionDbContext dbContext)
        {
            var instSubscription = dbContext.InstitutionSubscription.Find(ValidInstitutionSubscriptionId);

            dbContext.StudentSubscription.Add(new StudentSubscription
            {
                ID = 1,
                AccountID = ValidAccountId,
                ValidityStartDate = new DateTime(2021, 6, 1),
                ValidityPeriod = 90,
                GradYear = 2018,
                QuestionBank = dbContext.QuestionBank.Find(1),
                InstitutionSubscription = instSubscription
            });
            dbContext.StudentSubscription.Add(new StudentSubscription
            {
                ID = 2,
                AccountID = ValidAccountId,
                ValidityStartDate = new DateTime(2021, 1, 1),
                ValidityPeriod = 90,
                QuestionBank = dbContext.QuestionBank.Find(2),
                InstitutionSubscription = instSubscription
            });
            dbContext.StudentSubscription.Add(new StudentSubscription
            {
                ID = 3,
                AccountID = ValidAccountId,
                ValidityStartDate = new DateTime(2020, 9, 1),
                ValidityPeriod = 365,
                GradYear = 2019,
                QuestionBank = dbContext.QuestionBank.Find(3),
                InstitutionSubscription = instSubscription
            });
            dbContext.SaveChanges();
        }

        private void InitInstitutionSubscriptions(SubscriptionDbContext dbContext)
        {
            dbContext.InstitutionSubscription.Add(new InstitutionSubscription
            {
                InstitutionId = ValidInstitutionId,
                Id = ValidInstitutionSubscriptionId
            });
            dbContext.SaveChanges();
        }

        private void InitQuestionBanks(SubscriptionDbContext dbContext)
        {
            dbContext.QuestionBank.Add(new QuestionBank
            {
                ID = 1,
                Active = true,
                Name = "Bank-1",
                StartDate = new DateTime(2020, 7, 1),
                EndDate = new DateTime(2021, 6, 30),
                TimeStamp = new DateTime(2020, 9, 1)
            });
            dbContext.QuestionBank.Add(new QuestionBank
            {
                ID = 2,
                Active = true,
                Name = "Bank-2",
                StartDate = new DateTime(2020, 7, 1),
                EndDate = new DateTime(2021, 6, 30),
                TimeStamp = new DateTime(2020, 9, 1)
            });
            dbContext.QuestionBank.Add(new QuestionBank
            {
                ID = 3,
                Active = true,
                Name = "Bank-3",
                StartDate = new DateTime(2020, 7, 1),
                EndDate = new DateTime(2021, 6, 30),
                TimeStamp = new DateTime(2020, 9, 1)
            });
            dbContext.SaveChanges();
        }
    }
}
