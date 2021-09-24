using Demo.Microservice.App.Operations.GetSubscriptions;
using Demo.Microservice.Core.Common.Enum;
using Demo.Microservice.Core.Common.Model;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;
using MemberSubscriptionDto = Demo.Microservice.App.Operations.GetSubscriptions.GetSubscriptionResponse.StudentSubscription;

namespace Demo.Microservice.App.Test.GetLearnerSubscriptions
{
    public partial class GetSubscriptionsTest
    {
        private static IEnumerable<string> ValidSortingProperties()
        {
            yield return nameof(MemberSubscriptionDto.QuestionBankName);
            yield return nameof(MemberSubscriptionDto.ValidityStartDate);
            yield return nameof(MemberSubscriptionDto.ValidityEndDate);
            yield return nameof(MemberSubscriptionDto.GraduationYear);
        }

        private static int DefaultPageSize => 5;
        
        public static TheoryData<GetSubscriptionRequest, int[]> ValidRequestsWithoutSorting()
        {
            return new TheoryData<GetSubscriptionRequest, int[]>
            {
                {
                    new GetSubscriptionRequest
                    {
                        InstitutionId = ValidInstitutionId,
                        AccountId = ValidAccountId
                    },
                    new int[] { 1, 3 }
                }
            };
        }

        public static TheoryData<GetSubscriptionRequest, int[]> ValidRequestsWithSorting()
        {
            return new TheoryData<GetSubscriptionRequest, int[]>
            {
                {
                    new GetSubscriptionRequest
                    {
                        InstitutionId = ValidInstitutionId,
                        AccountId = ValidAccountId,
                        SubscriptionFilter = new QueryFilter
                        {
                            Page = 1,
                            PageSize = DefaultPageSize,
                            Sort = new Sort { Field = nameof(MemberSubscriptionDto.QuestionBankName), Order = Order.Asc }
                        }
                    },
                    new int[] { 1, 3 }
                },
                {
                    new GetSubscriptionRequest
                    {
                        InstitutionId = ValidInstitutionId,
                        AccountId = ValidAccountId,
                        SubscriptionFilter = new QueryFilter
                        {
                            Page = 1,
                            PageSize = DefaultPageSize,
                            Sort = new Sort { Field = nameof(MemberSubscriptionDto.QuestionBankName), Order = Order.Desc }
                        }
                    },
                    new int[] { 3, 1 }
                },
                {
                    new GetSubscriptionRequest
                    {
                        InstitutionId = ValidInstitutionId,
                        AccountId = ValidAccountId,
                        SubscriptionFilter = new QueryFilter
                        {
                            Page = 1,
                            PageSize = DefaultPageSize,
                            Sort = new Sort { Field = nameof(MemberSubscriptionDto.ValidityStartDate), Order = Order.Asc }
                        }
                    },
                    new int[] { 3, 1 }
                },
                {
                    new GetSubscriptionRequest
                    {
                        InstitutionId = ValidInstitutionId,
                        AccountId = ValidAccountId,
                        SubscriptionFilter = new QueryFilter
                        {
                            Page = 1,
                            PageSize = DefaultPageSize,
                            Sort = new Sort { Field = nameof(MemberSubscriptionDto.ValidityStartDate), Order = Order.Desc }
                        }
                    },
                    new int[] { 1, 3 }
                },
                {
                    new GetSubscriptionRequest
                    {
                        InstitutionId = ValidInstitutionId,
                        AccountId = ValidAccountId,
                        SubscriptionFilter = new QueryFilter
                        {
                            Page = 1,
                            PageSize = DefaultPageSize,
                            Sort = new Sort { Field = nameof(MemberSubscriptionDto.ValidityEndDate), Order = Order.Asc }
                        }
                    },
                    new int[] { 1, 3 }
                },
                {
                    new GetSubscriptionRequest
                    {
                        InstitutionId = ValidInstitutionId,
                        AccountId = ValidAccountId,
                        SubscriptionFilter = new QueryFilter
                        {
                            Page = 1,
                            PageSize = DefaultPageSize,
                            Sort = new Sort { Field = nameof(MemberSubscriptionDto.ValidityEndDate), Order = Order.Desc }
                        }
                    },
                    new int[] { 3, 1 }
                },
                {
                    new GetSubscriptionRequest
                    {
                        InstitutionId = ValidInstitutionId,
                        AccountId = ValidAccountId,
                        SubscriptionFilter = new QueryFilter
                        {
                            Page = 1,
                            PageSize = DefaultPageSize,
                            Sort = new Sort { Field = nameof(MemberSubscriptionDto.GraduationYear), Order = Order.Asc }
                        }
                    },
                    new int[] { 1, 3 }
                },
                {
                    new GetSubscriptionRequest
                    {
                        InstitutionId = ValidInstitutionId,
                        AccountId = ValidAccountId,
                        SubscriptionFilter = new QueryFilter
                        {
                            Page = 1,
                            PageSize = DefaultPageSize,
                            Sort = new Sort { Field = nameof(MemberSubscriptionDto.GraduationYear), Order = Order.Desc }
                        }
                    },
                    new int[] { 3, 1 }
                }
            };
        }
    }
}
