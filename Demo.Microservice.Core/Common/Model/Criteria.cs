
using Demo.Microservice.Core.Common.Enum;

namespace Demo.Microservice.Core.Common.Model
{
    public class Criteria
    {
        public string Field { get; set; }
        public string Value { get; set; }
        public Condition Condition { get; set; }
    }
}