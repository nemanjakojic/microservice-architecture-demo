using Demo.Microservice.App.Operations.CreateSubscriptions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using static Demo.Microservice.App.Operations.CreateSubscriptions.CreateSubscriptionsRequest;

namespace Demo.Microservice.App.Test.CreateLearnerSubscriptions
{
    public partial class CreateSubscriptionsTest
    {
        public static TheoryData<CreateSubscriptionsRequest> ValidRequests()
        {
            return new TheoryData<CreateSubscriptionsRequest>
            {
                new CreateSubscriptionsRequest
                {
                    InstitutionSubscriptionData = new InstitutionSubscription
                    {
                        QuestionBankId = 1,
                        Id = ValidInstitutionSubscriptionId,
                        StartDate = DateTime.MinValue,
                        EndDate = DateTime.MaxValue
                    },
                    Students = new List<StudentData>
                    {
                        new StudentData { AccountId = ValidAccountId, GraduationYear = 2020 }
                    }
                }
            };
        }
    }
}
