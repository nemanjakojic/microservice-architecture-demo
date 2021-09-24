using Demo.Microservice.App.Data.Context;
using Demo.Microservice.App.Operations.CreateSubscriptions;
using Demo.Microservice.Core.Test.Mock;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Demo.Microservice.App.Test.CreateLearnerSubscriptions
{
    public partial class CreateSubscriptionsTest
    {
        private ServiceCollection Services { get; set; }
        private ServiceProvider ServiceProvider { get; set; }

        public CreateSubscriptionsTest()
        {
            Services = new ServiceCollection();

            Services.AddDbContext<SubscriptionDbContext>(
                opt =>
                {
                    opt.UseInMemoryDatabase(databaseName: $"InMemoryDb-{ Guid.NewGuid() }");
                    opt.ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning));
                },
                ServiceLifetime.Scoped,
                ServiceLifetime.Scoped);

            ServiceProvider = Services.BuildServiceProvider();
        }

        [Theory]
        [MemberData(nameof(InvalidRequests), MemberType = typeof(CreateSubscriptionsTest))]
        public async Task TestExecute_WhenInputIsInvalid_ReturnsFailure(CreateSubscriptionRequest testRequest)
        {
            // Arrange
            var dbContext = InitDataModel();

            var appOperation = new CreateSubscriptionOperation(
                dbContext: dbContext,
                dateTimeService: MockUtils.MockDateTimeService(new DateTime(2021, 7, 10)),
                logger: MockUtils.MockLogger<CreateSubscriptionOperation>());

            // Act
            var result = await appOperation.Execute(testRequest);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.Completed);
        }

        [Theory]
        [MemberData(nameof(ValidRequests), MemberType = typeof(CreateSubscriptionsTest))]
        public async Task TestExecute_WhenInputIsValid_SubscriptionIsCreated(CreateSubscriptionRequest testRequest)
        {
            // Arrange
            var dbContext = InitDataModel();

            var appOperation = new CreateSubscriptionOperation(
                dbContext: dbContext,
                dateTimeService: MockUtils.MockDateTimeService(new DateTime(2021, 7, 10)),
                logger: MockUtils.MockLogger<CreateSubscriptionOperation>());

            // Act
            var result = await appOperation.Execute(testRequest);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Completed);

            var subscription = dbContext.StudentSubscription.Find(4);
            Assert.NotNull(subscription);
        }
    }
}
