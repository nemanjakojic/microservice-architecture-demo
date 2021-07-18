using Demo.Microservice.Core;
using Demo.Microservice.Core.Common.Model;
using System;
using System.Collections.Generic;

namespace Demo.Microservice.App.Operations.GetLearnerSubscriptions
{
    public class GetLearnerSubscriptionsResponse : ServiceResponse
    {
        public PagedResult<IEnumerable<MemberSubscription>> SearchResult { get; set; }

        public class MemberSubscription
        {
            public int ID { get; set; }
            public int AccountID { get; set; }
            public int? ExamBankID { get; set; }
            public int? ExamYearID { get; set; }
            public string ExamBankName { get; set; }
            public string ExamYearName { get; set; }
            public DateTime? ValidityStartDate { get; set; }
            public DateTime? ValidityEndDate { get; set; }
            public int? ValidityPeriod { get; set; }
            public Guid? InstitutionSubscriptionId { get; set; }
            public DateTime? LastLogin { get; set; }
            public bool? Active { get; set; }
            public int GraduationYear { get; set; }
        }
    }
}
