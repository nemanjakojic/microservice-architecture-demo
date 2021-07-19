using Demo.Microservice.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Microservice.App.Operations.CreateLearnerSubscriptions
{
    public class CreateLearnerSubscriptionsRequest : ServiceRequest
    {
        public InstitutionSubscription InstitutionSubscriptionData { get; set; }
        public IEnumerable<LearnerData> Learners { get; set; }

        public class InstitutionSubscription
        {
            public Guid InstitutionSubscriptionId { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
            public int ExamBankId { get; set; }
            public int ExamYearId { get; set; }
        }

        public class LearnerData
        {
            public int AccountId { get; set; }
            public int GraduationYear { get; set; }
        }

    }
}
