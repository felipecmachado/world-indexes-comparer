using EntityFrameworkCore.UnitOfWork.Interfaces;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using RestCountries.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WorldIndexesComparer.Application.Services;
using Xunit;

namespace WorldIndexesComparer.Tests.Application.Services
{
    public class CountriesProcessingServiceTests
    {
        private readonly Mock<ILogger<CountriesProcessingService>> _mockLogger;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IRestCountriesClient> _mockRestCountriesClient;

        private readonly CountriesProcessingService _countriesProcessingService;

        public CountriesProcessingServiceTests()
        {
            _mockLogger = new();
            _mockUnitOfWork = new();
            _mockRestCountriesClient = new();
            _countriesProcessingService = new(_mockLogger.Object, _mockUnitOfWork.Object, _mockRestCountriesClient.Object);
        }

        [Fact]
        public void Constructor_Initialize_ThrowArgumentNullException()
        {
            // Arrange

            // Act
            Action actionLoggerNull = () => _ = new CountriesProcessingService(null, _mockUnitOfWork.Object, _mockRestCountriesClient.Object);
            Action actionUnitOfWorkNull = () => _ = new CountriesProcessingService(_mockLogger.Object, null, _mockRestCountriesClient.Object);
            Action actionRestCountriesClientNull = () => _ = new CountriesProcessingService(_mockLogger.Object, _mockUnitOfWork.Object, null);

            // Assert
            actionLoggerNull.Should().Throw<ArgumentNullException>();
            actionUnitOfWorkNull.Should().Throw<ArgumentNullException>();
            actionRestCountriesClientNull.Should().Throw<ArgumentNullException>();
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
