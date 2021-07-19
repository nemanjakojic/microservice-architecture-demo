using System.Collections.Generic;

namespace Demo.Microservice.Core.Common.Model
{
    public class QueryFilter
    {
        public QueryFilter()
        {
            Criteria = new List<Criteria>();
        }

        public int Page { get; set; }
        public int PageSize { get; set; }
        public Sort Sort { get; set; }

        public List<Criteria> Criteria { get; set; }
    }
}
