using Demo.Microservice.App.Data.Context;
using Demo.Microservice.App.Operations.GetLearnerSubscriptions;
using Demo.Microservice.Core.Test.Mock;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Demo.Microservice.App.Test.GetLearnerSubscriptions
{
    public partial class GetLearnerSubscriptionsTest
    {
        private ServiceCollection Services { get; set; }
        private ServiceProvider ServiceProvider { get; set; }

        public GetLearnerSubscriptionsTest()
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
        [MemberData(nameof(ValidRequestsWithoutSorting), MemberType = typeof(GetLearnerSubscriptionsTest))]
        public async Task TestExecute_WhenInputIsValidAndNoSorting_ReturnsCorrectResult(GetLearnerSubscriptionsRequest request, int[] expectedResult)
        {
            // Arrange
            using var dbContext = InitDataModel();

            var appService = new GetLearnerSubscriptionsOperation(
                context: dbContext,
                dateTimeService: MockUtils.MockDateTimeService(new DateTime(2021, 7, 10)),
                logger: MockUtils.MockLogger<GetLearnerSubscriptionsOperation>());

            // Act
            var result = await appService.Execute(request, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Completed);
            Assert.NotEmpty(result.SearchResult.Items);
            Assert.Equal(2, result.SearchResult.Total);

            var subscriptions = result.SearchResult.Items.ToList();
            Assert.Contains(subscriptions, s => s.ID == expectedResult[0]);
            Assert.Contains(subscriptions, s => s.ID == expectedResult[1]);
        }

        [Theory]
        [MemberData(nameof(ValidRequestsWithSorting), MemberType = typeof(GetLearnerSubscriptionsTest))]
        public async Task TestExecute_WhenRequestIsValidAndSorting_ReturnsCorrectlySortedResult(GetLearnerSubscriptionsRequest request, int[] expectedResult)
        {
            // Arrange
            using var dbContext = InitDataModel();

            var appService = new GetLearnerSubscriptionsOperation(
                context: dbContext,
                dateTimeService: MockUtils.MockDateTimeService(new DateTime(2021, 7, 10)),
                logger: MockUtils.MockLogger<GetLearnerSubscriptionsOperation>());

            // Act
            var result = await appService.Execute(request, CancellationToken.None);

            // Assert result metadata
            Assert.NotNull(result);
            Assert.True(result.Completed);
            Assert.NotEmpty(result.SearchResult.Items);
            Assert.Equal(2, result.SearchResult.Total);

            // Assert order
            var subscriptions = result.SearchResult.Items.ToList();
            Assert.Equal(expectedResult[0], subscriptions[0].ID);
            Assert.Equal(expectedResult[1], subscriptions[1].ID);
        }

        [Theory]
        [MemberData(nameof(InvalidRequests), MemberType = typeof(GetLearnerSubscriptionsTest))]
        public async Task TestExecute_WhenInputIsInvalid_ReturnsFailure(GetLearnerSubscriptionsRequest testRequest)
        {
            // Arrange
            using var dbContext = InitDataModel();

            var appOperation = new GetLearnerSubscriptionsOperation(
                context: dbContext,
                dateTimeService: MockUtils.MockDateTimeService(new DateTime(2021, 7, 10)),
                logger: MockUtils.MockLogger<GetLearnerSubscriptionsOperation>());

            // Act
            var result = await appOperation.Execute(testRequest, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.Completed);
            Assert.Null(result.SearchResult);
        }
    }
}