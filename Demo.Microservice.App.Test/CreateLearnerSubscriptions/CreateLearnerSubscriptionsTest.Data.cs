using Demo.Microservice.App.Data.Context;
using Demo.Microservice.App.Data.Entity;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Demo.Microservice.App.Test.CreateLearnerSubscriptions
{
    public partial class CreateLearnerSubscriptionsTest
    {
        private static readonly Guid ValidInstitutionId = Guid.Parse("A1A1A1A1-A1A1-A1A1-A1A1-A1A1A1A1A1A1");
        private static readonly Guid ValidInstitutionSubscriptionId = Guid.Parse("B1B1B1B1-B1B1-B1B1-B1B1-B1B1B1B1B1B1");
        private const int ValidAccountId = 111111;

        private SubscriptionDbContext InitDataModel()
        {
            var dbContext = ServiceProvider.GetRequiredService<SubscriptionDbContext>();
            InitInstitutionSubscriptions(dbContext);
            InitExamYears(dbContext);
            InitExamBanks(dbContext);
            InitMemberSubscriptions(dbContext);
            return dbContext;
        }

        private void InitMemberSubscriptions(SubscriptionDbContext dbContext)
        {
            var instSubscription = dbContext.InstitutionSubscription.Find(ValidInstitutionSubscriptionId);

            dbContext.MemberSubscription.Add(new MemberSubscription
            {
                ID = 1,
                AccountID = ValidAccountId,
                ValidityStartDate = new DateTime(2021, 6, 1),
                ValidityPeriod = 90,
                GradYear = 2018,
                ExamBank = dbContext.ExamBank.Find(1),
                ExamYear = dbContext.ExamYear.Find(2),
                InstitutionSubscription = instSubscription
            });
            dbContext.MemberSubscription.Add(new MemberSubscription
            {
                ID = 2,
                AccountID = ValidAccountId,
                ValidityStartDate = new DateTime(2021, 1, 1),
                ValidityPeriod = 90,
                ExamBank = dbContext.ExamBank.Find(2),
                ExamYear = dbContext.ExamYear.Find(1),
                InstitutionSubscription = instSubscription
            });
            dbContext.MemberSubscription.Add(new MemberSubscription
            {
                ID = 3,
                AccountID = ValidAccountId,
                ValidityStartDate = new DateTime(2020, 9, 1),
                ValidityPeriod = 365,
                GradYear = 2019,
                ExamBank = dbContext.ExamBank.Find(3),
                ExamYear = dbContext.ExamYear.Find(1),
                InstitutionSubscription = instSubscription
            });
            dbContext.SaveChanges();
        }

        private void InitInstitutionSubscriptions(SubscriptionDbContext dbContext)
        {
            dbContext.InstitutionSubscription.Add(new InstitutionSubscription
            {
                InstitutionNodeId = ValidInstitutionId,
                InstitutionSubscriptionId = ValidInstitutionSubscriptionId
            });
            dbContext.SaveChanges();
        }

        private void InitExamYears(SubscriptionDbContext dbContext)
        {
            dbContext.ExamYear.Add(new ExamYear
            {
                ID = 1,
                Name = "AY 2019-2020",
                Start = 2019,
                Finish = 2020
            });
            dbContext.Add(new ExamYear
            {
                ID = 2,
                Name = "AY 2020-2021",
                Start = 2020,
                Finish = 2021
            });
            dbContext.SaveChanges();
        }

        private void InitExamBanks(SubscriptionDbContext dbContext)
        {
            dbContext.ExamBank.Add(new ExamBank
            {
                ID = 1,
                Active = true,
                Name = "Bank-1",
                StartDate = new DateTime(2020, 7, 1),
                EndDate = new DateTime(2021, 6, 30),
                TimeStamp = new DateTime(2020, 9, 1)
            });
            dbContext.ExamBank.Add(new ExamBank
            {
                ID = 2,
                Active = true,
                Name = "Bank-2",
                StartDate = new DateTime(2020, 7, 1),
                EndDate = new DateTime(2021, 6, 30),
                TimeStamp = new DateTime(2020, 9, 1)
            });
            dbContext.ExamBank.Add(new ExamBank
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
