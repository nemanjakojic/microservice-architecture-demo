using Demo.Microservice.Core;
using Demo.Microservice.Core.Common.Model;
using System;
using System.Collections.Generic;

namespace Demo.Microservice.App.Operations.GetSubscriptions
{
    public class GetSubscriptionsResponse : ServiceResponse
    {
        public PagedResult<IEnumerable<StudentSubscription>> SearchResult { get; set; }

        public class StudentSubscription
        {
            public int ID { get; set; }
            public int AccountID { get; set; }
            public int? QuestionBankID { get; set; }
            public string QuestionBankName { get; set; }
            public DateTime? ValidityStartDate { get; set; }
            public DateTime? ValidityEndDate { get; set; }
            public Guid? InstitutionSubscriptionId { get; set; }
            public bool? Active { get; set; }
            public int GraduationYear { get; set; }
        }
    }
}
