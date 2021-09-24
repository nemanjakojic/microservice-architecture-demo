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
        public static TheoryData<CreateSubscriptionRequest> InvalidRequests()
        {
            return new TheoryData<CreateSubscriptionRequest>
            {
                null,
                new CreateSubscriptionRequest
                {
                    InstitutionSubscriptionData = null,
                    Students = null
                },
                new CreateSubscriptionRequest
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
                new CreateSubscriptionRequest
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
                new CreateSubscriptionRequest
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
                new CreateSubscriptionRequest
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
                new CreateSubscriptionRequest
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
                new CreateSubscriptionRequest
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
                new CreateSubscriptionRequest
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
                new CreateSubscriptionRequest
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
                new CreateSubscriptionRequest
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
                new CreateSubscriptionRequest
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
