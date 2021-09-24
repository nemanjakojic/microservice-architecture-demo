using Demo.Microservice.App.Operations.CreateSubscriptions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using static Demo.Microservice.App.Operations.CreateSubscriptions.CreateSubscriptionRequest;

namespace Demo.Microservice.App.Test.CreateLearnerSubscriptions
{
    public partial class CreateSubscriptionsTest
    {
        public static TheoryData<CreateSubscriptionRequest> ValidRequests()
        {
            return new TheoryData<CreateSubscriptionRequest>
            {
                new CreateSubscriptionRequest
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
