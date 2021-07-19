using Demo.Microservice.App.Data.Context;
using Demo.Microservice.App.Operations.CreateLearnerSubscriptions;
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
    public partial class CreateLearnerSubscriptionsTest
    {
        private ServiceCollection Services { get; set; }
        private ServiceProvider ServiceProvider { get; set; }

        public CreateLearnerSubscriptionsTest()
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
        [MemberData(nameof(InvalidRequests), MemberType = typeof(CreateLearnerSubscriptionsTest))]
        public async Task TestExecute_WhenInputIsInvalid_ReturnsFailure(CreateLearnerSubscriptionsRequest testRequest)
        {
            // Arrange
            var dbContext = InitDataModel();

            var appOperation = new CreateLearnerSubscriptionsOperation(
                dbContext: dbContext,
                dateTimeService: MockUtils.MockDateTimeService(new DateTime(2021, 7, 10)),
                logger: MockUtils.MockLogger<CreateLearnerSubscriptionsOperation>());

            // Act
            var result = await appOperation.Execute(testRequest, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.Completed);
        }

        [Theory]
        [MemberData(nameof(ValidRequests), MemberType = typeof(CreateLearnerSubscriptionsTest))]
        public async Task TestExecute_WhenInputIsValid_SubscriptionIsCreated(CreateLearnerSubscriptionsRequest testRequest)
        {
            // Arrange
            var dbContext = InitDataModel();

            var appOperation = new CreateLearnerSubscriptionsOperation(
                dbContext: dbContext,
                dateTimeService: MockUtils.MockDateTimeService(new DateTime(2021, 7, 10)),
                logger: MockUtils.MockLogger<CreateLearnerSubscriptionsOperation>());

            // Act
            var result = await appOperation.Execute(testRequest, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Completed);

            var subscription = dbContext.MemberSubscription.Find(4);
            Assert.NotNull(subscription);
        }
    }
}
