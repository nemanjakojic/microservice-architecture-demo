using Demo.Microservice.App.Operations.GetLearnerSubscriptions;
using Demo.Microservice.Core.Common.Enum;
using Demo.Microservice.Core.Common.Model;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;
using MemberSubscriptionDto = Demo.Microservice.App.Operations.GetLearnerSubscriptions.GetLearnerSubscriptionsResponse.MemberSubscription;

namespace Demo.Microservice.App.Test.GetLearnerSubscriptions
{
    public partial class GetLearnerSubscriptionsTest
    {
        private static IEnumerable<string> ValidSortingProperties()
        {
            yield return nameof(MemberSubscriptionDto.ExamBankName);
            yield return nameof(MemberSubscriptionDto.ExamYearName);
            yield return nameof(MemberSubscriptionDto.ValidityStartDate);
            yield return nameof(MemberSubscriptionDto.ValidityEndDate);
            yield return nameof(MemberSubscriptionDto.GraduationYear);
        }

        private static int DefaultPageSize => 5;
        
        public static TheoryData<GetLearnerSubscriptionsRequest, int[]> ValidRequestsWithoutSorting()
        {
            return new TheoryData<GetLearnerSubscriptionsRequest, int[]>
            {
                {
                    new GetLearnerSubscriptionsRequest
                    {
                        InstitutionId = ValidInstitutionId,
                        AccountId = ValidAccountId
                    },
                    new int[] { 1, 3 }
                }
            };
        }

        public static TheoryData<GetLearnerSubscriptionsRequest, int[]> ValidRequestsWithSorting()
        {
            return new TheoryData<GetLearnerSubscriptionsRequest, int[]>
            {
                {
                    new GetLearnerSubscriptionsRequest
                    {
                        InstitutionId = ValidInstitutionId,
                        AccountId = ValidAccountId,
                        SubscriptionFilter = new QueryFilter
                        {
                            Page = 1,
                            PageSize = DefaultPageSize,
                            Sort = new Sort { Field = nameof(MemberSubscriptionDto.ExamBankName), Order = Order.Asc }
                        }
                    },
                    new int[] { 1, 3 }
                },
                {
                    new GetLearnerSubscriptionsRequest
                    {
                        InstitutionId = ValidInstitutionId,
                        AccountId = ValidAccountId,
                        SubscriptionFilter = new QueryFilter
                        {
                            Page = 1,
                            PageSize = DefaultPageSize,
                            Sort = new Sort { Field = nameof(MemberSubscriptionDto.ExamBankName), Order = Order.Desc }
                        }
                    },
                    new int[] { 3, 1 }
                },
                {
                    new GetLearnerSubscriptionsRequest
                    {
                        InstitutionId = ValidInstitutionId,
                        AccountId = ValidAccountId,
                        SubscriptionFilter = new QueryFilter
                        {
                            Page = 1,
                            PageSize = DefaultPageSize,
                            Sort = new Sort { Field = nameof(MemberSubscriptionDto.ExamYearName), Order = Order.Asc }
                        }
                    },
                    new int[] { 3, 1 }
                },
                {
                    new GetLearnerSubscriptionsRequest
                    {
                        InstitutionId = ValidInstitutionId,
                        AccountId = ValidAccountId,
                        SubscriptionFilter = new QueryFilter
                        {
                            Page = 1,
                            PageSize = DefaultPageSize,
                            Sort = new Sort { Field = nameof(MemberSubscriptionDto.ExamYearName), Order = Order.Desc }
                        }
                    },
                    new int[] { 1, 3 }
                },
                {
                    new GetLearnerSubscriptionsRequest
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
                    new GetLearnerSubscriptionsRequest
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
                    new GetLearnerSubscriptionsRequest
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
                    new GetLearnerSubscriptionsRequest
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
                    new GetLearnerSubscriptionsRequest
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
                    new GetLearnerSubscriptionsRequest
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
