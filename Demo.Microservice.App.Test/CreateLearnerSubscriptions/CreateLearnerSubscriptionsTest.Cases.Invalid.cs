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
        public static TheoryData<CreateLearnerSubscriptionsRequest> InvalidRequests()
        {
            return new TheoryData<CreateLearnerSubscriptionsRequest>
            {
                null,
                new CreateLearnerSubscriptionsRequest
                {
                    InstitutionSubscriptionData = null,
                    Learners = null
                },
                new CreateLearnerSubscriptionsRequest
                {
                    InstitutionSubscriptionData = new InstitutionSubscription
                    {
                        ExamBankId = 0,
                        ExamYearId = 0,
                        InstitutionSubscriptionId = Guid.Empty,
                        StartDate = DateTime.MinValue,
                        EndDate = DateTime.MaxValue
                    },
                    Learners = null
                },
                new CreateLearnerSubscriptionsRequest
                {
                    InstitutionSubscriptionData = new InstitutionSubscription
                    {
                        ExamBankId = 0,
                        ExamYearId = 0,
                        InstitutionSubscriptionId = ValidInstitutionSubscriptionId,
                        StartDate = DateTime.MinValue,
                        EndDate = DateTime.MaxValue
                    },
                    Learners = null
                },
                new CreateLearnerSubscriptionsRequest
                {
                    InstitutionSubscriptionData = new InstitutionSubscription
                    {
                        ExamBankId = 1,
                        ExamYearId = 0,
                        InstitutionSubscriptionId = ValidInstitutionSubscriptionId,
                        StartDate = DateTime.MinValue,
                        EndDate = DateTime.MaxValue
                    },
                    Learners = null
                },
                new CreateLearnerSubscriptionsRequest
                {
                    InstitutionSubscriptionData = new InstitutionSubscription
                    {
                        ExamBankId = 0,
                        ExamYearId = 1,
                        InstitutionSubscriptionId = ValidInstitutionSubscriptionId,
                        StartDate = DateTime.MinValue,
                        EndDate = DateTime.MaxValue
                    },
                    Learners = null
                },
                new CreateLearnerSubscriptionsRequest
                {
                    InstitutionSubscriptionData = new InstitutionSubscription
                    {
                        ExamBankId = 1,
                        ExamYearId = 1,
                        InstitutionSubscriptionId = Guid.Empty,
                        StartDate = DateTime.MinValue,
                        EndDate = DateTime.MaxValue
                    },
                    Learners = null
                },
                new CreateLearnerSubscriptionsRequest
                {
                    InstitutionSubscriptionData = new InstitutionSubscription
                    {
                        ExamBankId = 1,
                        ExamYearId = 1,
                        InstitutionSubscriptionId = Guid.Empty,
                        StartDate = DateTime.MinValue,
                        EndDate = DateTime.MaxValue
                    },
                    Learners = new List<LearnerData>()
                },
                new CreateLearnerSubscriptionsRequest
                {
                    InstitutionSubscriptionData = new InstitutionSubscription
                    {
                        ExamBankId = 1,
                        ExamYearId = 1,
                        InstitutionSubscriptionId = Guid.Empty,
                        StartDate = DateTime.MinValue,
                        EndDate = DateTime.MaxValue
                    },
                    Learners = new List<LearnerData> { null, null }
                },
                new CreateLearnerSubscriptionsRequest
                {
                    InstitutionSubscriptionData = new InstitutionSubscription
                    {
                        ExamBankId = 1,
                        ExamYearId = 1,
                        InstitutionSubscriptionId = Guid.Empty,
                        StartDate = DateTime.MinValue,
                        EndDate = DateTime.MaxValue
                    },
                    Learners = new List<LearnerData>
                    {
                        new LearnerData { AccountId = 0, GraduationYear = 0 },
                        new LearnerData { AccountId = 0, GraduationYear = 0 }
                    }
                },
                new CreateLearnerSubscriptionsRequest
                {
                    InstitutionSubscriptionData = new InstitutionSubscription
                    {
                        ExamBankId = 1,
                        ExamYearId = 1,
                        InstitutionSubscriptionId = Guid.Empty,
                        StartDate = DateTime.MinValue,
                        EndDate = DateTime.MaxValue
                    },
                    Learners = new List<LearnerData>
                    {
                        new LearnerData { AccountId = ValidAccountId, GraduationYear = 0 }
                    }
                },
                new CreateLearnerSubscriptionsRequest
                {
                    InstitutionSubscriptionData = new InstitutionSubscription
                    {
                        ExamBankId = 1,
                        ExamYearId = 1,
                        InstitutionSubscriptionId = Guid.Empty,
                        StartDate = DateTime.MinValue,
                        EndDate = DateTime.MaxValue
                    },
                    Learners = new List<LearnerData>
                    {
                        new LearnerData { AccountId = 0, GraduationYear = 2020 }
                    }
                }
            };
        }
    }
}
