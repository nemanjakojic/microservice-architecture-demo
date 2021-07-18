using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Microservice.Core.Service
{
    public interface IDateTimeService
    {
        DateTime UtcNow();
    }
}
