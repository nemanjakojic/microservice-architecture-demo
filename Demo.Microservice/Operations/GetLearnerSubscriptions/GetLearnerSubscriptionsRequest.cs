using Demo.Microservice.Core;
using Demo.Microservice.Core.Common.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Microservice.App.Operations.GetLearnerSubscriptions
{
    public class GetLearnerSubscriptionsRequest : ServiceRequest
    {
        public Guid InstitutionId { get; set; }
        public int AccountId { get; set; }
        public QueryFilter SubscriptionFilter { get; set; }
    }
}
