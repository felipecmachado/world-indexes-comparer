using Covid19.Client;
using FluentAssertions;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using RestCountries.Client;
using System;
using System.Threading.Tasks;
using WorldIndexesComparer.Application.Services;
using Xunit;

namespace WorldIndexesComparer.Tests.Application.Services
{
    public class CountriesProcessingServiceTests
    {
        private readonly Mock<ILogger<CountriesProcessingAppService>> _mockLogger;
        private readonly Mock<IRestCountriesClient> _mockRestCountriesClient;
        private readonly Mock<ICoronavirusClient> _mockCoronavirusClient;
        private readonly Mock<IMediator> _mediator;

        private readonly CountriesProcessingAppService _countriesProcessingService;

        public CountriesProcessingServiceTests()
        {
            _mockLogger = new();
            _mediator = new();
            _mockRestCountriesClient = new();
            _mockCoronavirusClient = new();
            _countriesProcessingService = new(_mockLogger.Object, _mockRestCountriesClient.Object, _mockCoronavirusClient.Object, _mediator.Object);
        }

        [Fact]
        public void Constructor_Initialize_ThrowArgumentNullException()
        {
            // Arrange

            // Act
            Action actionLoggerNull = () => _ = new CountriesProcessingAppService(null, _mockRestCountriesClient.Object, _mockCoronavirusClient.Object, _mediator.Object);
            Action actionRestCountriesClientNull = () => _ = new CountriesProcessingAppService(_mockLogger.Object, null, _mockCoronavirusClient.Object, _mediator.Object);
            Action actionCoronavirusClientNull = () => _ = new CountriesProcessingAppService(_mockLogger.Object, _mockRestCountriesClient.Object, null, _mediator.Object);
            Action actionMediatorNull = () => _ = new CountriesProcessingAppService(_mockLogger.Object, _mockRestCountriesClient.Object, _mockCoronavirusClient.Object, null);

            // Assert
            actionLoggerNull.Should().Throw<ArgumentNullException>();
            actionRestCountriesClientNull.Should().Throw<ArgumentNullException>();
            actionCoronavirusClientNull.Should().Throw<ArgumentNullException>();
            actionMediatorNull.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public async Task SyncAllCountriesAsync_ValidData_RunSuccessfully()
        {
            // Arrange

            // Act
            // await _countriesProcessingService.SyncAllCountriesAsync(new CancellationToken());

            // Assert
        }
    }
}
