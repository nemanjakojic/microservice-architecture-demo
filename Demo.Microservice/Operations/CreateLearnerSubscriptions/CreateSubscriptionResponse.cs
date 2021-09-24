using Demo.Microservice.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Microservice.App.Operations.CreateSubscriptions
{
    public class CreateSubscriptionResponse : ServiceResponse
    {
        public IEnumerable<StudentSubscription> CreatedSubscriptions { get; set; }

        public class StudentSubscription
        {
            public int LearnerSubscriptionId { get; set; }
            public int LearnerAccountId { get; set; }
        }
    }
}
