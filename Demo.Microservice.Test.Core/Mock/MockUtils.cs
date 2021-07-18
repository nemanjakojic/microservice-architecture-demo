using Demo.Microservice.Core.Service;
using Microsoft.Extensions.Logging;
using Moq;
using System;

namespace Demo.Microservice.Core.Test.Mock
{
    public class MockUtils
    {
        public static IDateTimeService MockDateTimeService(DateTime utcNow)
        {
            var mock = new Mock<IDateTimeService>();
            mock.Setup(m => m.UtcNow()).Returns(utcNow);
            return mock.Object;
        }

        public static ILogger<T> MockLogger<T>()
        {
            var mockLogger = new Mock<ILogger<T>>();
            return mockLogger.Object;
        }
    }
}
