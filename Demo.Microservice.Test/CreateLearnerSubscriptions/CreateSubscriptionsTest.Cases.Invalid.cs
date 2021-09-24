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
        public static TheoryData<CreateSubscriptionsRequest> InvalidRequests()
        {
            return new TheoryData<CreateSubscriptionsRequest>
            {
                null,
                new CreateSubscriptionsRequest
                {
                    InstitutionSubscriptionData = null,
                    Students = null
                },
                new CreateSubscriptionsRequest
                {
                    InstitutionSubscriptionData = new InstitutionSubscription
                    {
                        QuestionBankId = 0,
                        Id = Guid.Empty,
                        StartDate = DateTime.MinValue,
                        EndDate = DateTime.MaxValue
                    },
                    Students = null
                },
                new CreateSubscriptionsRequest
                {
                    InstitutionSubscriptionData = new InstitutionSubscription
                    {
                        QuestionBankId = 0,
                        Id = ValidInstitutionSubscriptionId,
                        StartDate = DateTime.MinValue,
                        EndDate = DateTime.MaxValue
                    },
                    Students = null
                },
                new CreateSubscriptionsRequest
                {
                    InstitutionSubscriptionData = new InstitutionSubscription
                    {
                        QuestionBankId = 1,
                        Id = ValidInstitutionSubscriptionId,
                        StartDate = DateTime.MinValue,
                        EndDate = DateTime.MaxValue
                    },
                    Students = null
                },
                new CreateSubscriptionsRequest
                {
                    InstitutionSubscriptionData = new InstitutionSubscription
                    {
                        QuestionBankId = 0,
                        Id = ValidInstitutionSubscriptionId,
                        StartDate = DateTime.MinValue,
                        EndDate = DateTime.MaxValue
                    },
                    Students = null
                },
                new CreateSubscriptionsRequest
                {
                    InstitutionSubscriptionData = new InstitutionSubscription
                    {
                        QuestionBankId = 1,
                        Id = Guid.Empty,
                        StartDate = DateTime.MinValue,
                        EndDate = DateTime.MaxValue
                    },
                    Students = null
                },
                new CreateSubscriptionsRequest
                {
                    InstitutionSubscriptionData = new InstitutionSubscription
                    {
                        QuestionBankId = 1,
                        Id = Guid.Empty,
                        StartDate = DateTime.MinValue,
                        EndDate = DateTime.MaxValue
                    },
                    Students = new List<StudentData>()
                },
                new CreateSubscriptionsRequest
                {
                    InstitutionSubscriptionData = new InstitutionSubscription
                    {
                        QuestionBankId = 1,
                        Id = Guid.Empty,
                        StartDate = DateTime.MinValue,
                        EndDate = DateTime.MaxValue
                    },
                    Students = new List<StudentData> { null, null }
                },
                new CreateSubscriptionsRequest
                {
                    InstitutionSubscriptionData = new InstitutionSubscription
                    {
                        QuestionBankId = 1,
                        Id = Guid.Empty,
                        StartDate = DateTime.MinValue,
                        EndDate = DateTime.MaxValue
                    },
                    Students = new List<StudentData>
                    {
                        new StudentData { AccountId = 0, GraduationYear = 0 },
                        new StudentData { AccountId = 0, GraduationYear = 0 }
                    }
                },
                new CreateSubscriptionsRequest
                {
                    InstitutionSubscriptionData = new InstitutionSubscription
                    {
                        QuestionBankId = 1,
                        Id = Guid.Empty,
                        StartDate = DateTime.MinValue,
                        EndDate = DateTime.MaxValue
                    },
                    Students = new List<StudentData>
                    {
                        new StudentData { AccountId = ValidAccountId, GraduationYear = 0 }
                    }
                },
                new CreateSubscriptionsRequest
                {
                    InstitutionSubscriptionData = new InstitutionSubscription
                    {
                        QuestionBankId = 1,
                        Id = Guid.Empty,
                        StartDate = DateTime.MinValue,
                        EndDate = DateTime.MaxValue
                    },
                    Students = new List<StudentData>
                    {
                        new StudentData { AccountId = 0, GraduationYear = 2020 }
                    }
                }
            };
        }
    }
}
