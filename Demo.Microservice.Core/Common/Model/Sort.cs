
using Demo.Microservice.Core.Common.Enum;

namespace Demo.Microservice.Core.Common.Model
{
    public class Sort
    {
        /// <summary>
        /// Example:  "FirstName"
        /// </summary>
        public string Field { get; set; }

        public Order Order { get; set; }
    }
}
