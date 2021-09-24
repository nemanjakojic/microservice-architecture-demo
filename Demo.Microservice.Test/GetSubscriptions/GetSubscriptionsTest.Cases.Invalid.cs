using Demo.Microservice.App.Operations.GetSubscriptions;
using Demo.Microservice.Core.Common.Enum;
using Demo.Microservice.Core.Common.Model;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Demo.Microservice.App.Test.GetLearnerSubscriptions
{
    public partial class GetSubscriptionsTest
    {
        public static TheoryData<GetSubscriptionRequest> InvalidRequests()
        {
            return new TheoryData<GetSubscriptionRequest>
            {
                null,
                new GetSubscriptionRequest
                {
                    InstitutionId = Guid.Empty,
                    AccountId = ValidAccountId
                },
                new GetSubscriptionRequest
                {
                    InstitutionId = ValidInstitutionId,
                    AccountId = 0
                },
                new GetSubscriptionRequest
                {
                    InstitutionId = ValidInstitutionId,
                    AccountId = ValidAccountId,
                    SubscriptionFilter = new QueryFilter
                    {
                        Page = 0, // invalid page
                        PageSize = DefaultPageSize   // valid size
                    }
                },
                new GetSubscriptionRequest
                {
                    InstitutionId = ValidInstitutionId,
                    AccountId = ValidAccountId,
                    SubscriptionFilter = new QueryFilter
                    {
                        Page = 1, // valid page
                        PageSize = -1  // invalid size
                    }
                },
                new GetSubscriptionRequest
                {
                    InstitutionId = ValidInstitutionId,
                    AccountId = ValidAccountId,
                    SubscriptionFilter = new QueryFilter
                    {
                        Page = 0, // invalid page
                        PageSize = -1   // invalid size
                    }
                },
                new GetSubscriptionRequest
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
                new GetSubscriptionRequest
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
                new GetSubscriptionRequest
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
