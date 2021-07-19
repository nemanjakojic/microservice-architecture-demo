using Demo.Microservice.App.Operations.CreateLearnerSubscriptions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using static Demo.Microservice.App.Operations.CreateLearnerSubscriptions.CreateLearnerSubscriptionsRequest;

namespace Demo.Microservice.App.Test.CreateLearnerSubscriptions
{
    public partial class CreateLearnerSubscriptionsTest
    {
        public static TheoryData<CreateLearnerSubscriptionsRequest> ValidRequests()
        {
            return new TheoryData<CreateLearnerSubscriptionsRequest>
            {
                new CreateLearnerSubscriptionsRequest
                {
                    InstitutionSubscriptionData = new InstitutionSubscription
                    {
                        ExamBankId = 1,
                        ExamYearId = 1,
                        InstitutionSubscriptionId = ValidInstitutionSubscriptionId,
                        StartDate = DateTime.MinValue,
                        EndDate = DateTime.MaxValue
                    },
                    Learners = new List<LearnerData>
                    {
                        new LearnerData { AccountId = ValidAccountId, GraduationYear = 2020 }
                    }
                }
            };
        }
    }
}
