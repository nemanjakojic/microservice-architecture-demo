using Demo.Microservice.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Microservice.App.Operations.CreateSubscriptions
{
    public class CreateSubscriptionRequest : ServiceRequest
    {
        public InstitutionSubscription InstitutionSubscriptionData { get; set; }
        public IEnumerable<StudentData> Students { get; set; }

        public class InstitutionSubscription
        {
            public Guid Id { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
            public int QuestionBankId { get; set; }
        }

        public class StudentData
        {
            public int AccountId { get; set; }
            public int GraduationYear { get; set; }
        }

    }
}
