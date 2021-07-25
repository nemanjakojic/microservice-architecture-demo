using Demo.Microservice.App.Operations.GetLearnerSubscriptions;
using Demo.Microservice.Core.Common.Enum;
using Demo.Microservice.Core.Common.Model;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Demo.Microservice.App.Test.GetLearnerSubscriptions
{
    public partial class GetLearnerSubscriptionsTest
    {
        public static TheoryData<GetLearnerSubscriptionsRequest> InvalidRequests()
        {
            return new TheoryData<GetLearnerSubscriptionsRequest>
            {
                null,
                new GetLearnerSubscriptionsRequest
                {
                    InstitutionId = Guid.Empty,
                    AccountId = ValidAccountId
                },
                new GetLearnerSubscriptionsRequest
                {
                    InstitutionId = ValidInstitutionId,
                    AccountId = 0
                },
                new GetLearnerSubscriptionsRequest
                {
                    InstitutionId = ValidInstitutionId,
                    AccountId = ValidAccountId,
                    SubscriptionFilter = new QueryFilter
                    {
                        Page = 0, // invalid page
                        PageSize = DefaultPageSize   // valid size
                    }
                },
                new GetLearnerSubscriptionsRequest
                {
                    InstitutionId = ValidInstitutionId,
                    AccountId = ValidAccountId,
                    SubscriptionFilter = new QueryFilter
                    {
                        Page = 1, // valid page
                        PageSize = -1  // invalid size
                    }
                },
                new GetLearnerSubscriptionsRequest
                {
                    InstitutionId = ValidInstitutionId,
                    AccountId = ValidAccountId,
                    SubscriptionFilter = new QueryFilter
                    {
                        Page = 0, // invalid page
                        PageSize = -1   // invalid size
                    }
                },
                new GetLearnerSubscriptionsRequest
                {
                    InstitutionId = ValidInstitutionId,
                    AccountId = ValidAccountId,
                    SubscriptionFilter = new QueryFilter
                    {
                        Page = 1,  // valid page
                        PageSize = DefaultPageSize, // valid size
                        Sort = new Sort
                        {
                            Field = null,
                            Order = Order.Asc
                        }
                    }
                },
                new GetLearnerSubscriptionsRequest
                {
                    InstitutionId = ValidInstitutionId,
                    AccountId = ValidAccountId,
                    SubscriptionFilter = new QueryFilter
                    {
                        Page = 1,  // valid page
                        PageSize = DefaultPageSize, // valid size
                        Sort = new Sort
                        {
                            Field = "_InvalidFieldName_",
                            Order = Order.Asc
                        }
                    }
                },
                new GetLearnerSubscriptionsRequest
                {
                    InstitutionId = ValidInstitutionId,
                    AccountId = ValidAccountId,
                    SubscriptionFilter = new QueryFilter
                    {
                        Page = 1,  // valid page
                        PageSize = DefaultPageSize, // valid size
                        Sort = new Sort
                        {
                            Field = "ExamBankName", // valid field name
                            Order = (Order)100 // Invalid sorting order
                        }
                    }
                }
            };
        }
    }
}
