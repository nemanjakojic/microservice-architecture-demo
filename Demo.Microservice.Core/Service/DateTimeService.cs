using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Microservice.Core.Service
{
    internal class DateTimeService : IDateTimeService
    {
        public DateTime UtcNow()
        {
            return DateTime.UtcNow;
        }
    }
}
