using Demo.Microservice.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Microservice.App.Operations.CreateLearnerSubscriptions
{
    public class CreateLearnerSubscriptionResponse : ServiceResponse
    {
        public IEnumerable<LearnerSubscription> CreatedSubscriptions { get; set; }

        public class LearnerSubscription
        {
            public int LearnerSubscriptionId { get; set; }
            public int LearnerAccountId { get; set; }
        }
    }
}
