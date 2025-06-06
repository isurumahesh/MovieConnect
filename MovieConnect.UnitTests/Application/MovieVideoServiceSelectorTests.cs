using Moq;
using MovieConnect.Application.Interfaces;
using MovieConnect.Application.Services;
using MovieConnect.Core.Interfaces;

namespace MovieConnect.UnitTests.Application
{
    public class MovieVideoServiceSelectorTests
    {
        [Fact]
        public void GetService_ReturnsCorrectService_WhenProviderIsDefined()
        {
            // Arrange
            var service1Mock = new Mock<IMovieVideoService>();
            service1Mock.SetupGet(s => s.ProviderName).Returns("ProviderA");

            var service2Mock = new Mock<IMovieVideoService>();
            service2Mock.SetupGet(s => s.ProviderName).Returns("ProviderB");

            var services = new List<IMovieVideoService> { service1Mock.Object, service2Mock.Object };

            var providerConfigMock = new Mock<IMovieProviderConfiguration>();
            providerConfigMock.Setup(c => c.GetActiveMovieVideoProvider()).Returns("ProviderB");

            var selector = new MovieVideoServiceSelector(services, providerConfigMock.Object);

            // Act
            var selectedService = selector.GetService();

            // Assert
            Assert.NotNull(selectedService);
            Assert.Equal("ProviderB", selectedService.ProviderName);
        }

        [Fact]
        public void GetService_ThrowsArgumentException_WhenSelectedProviderIsNullOrEmpty()
        {
            // Arrange
            var services = new List<IMovieVideoService>();

            var providerConfigMock = new Mock<IMovieProviderConfiguration>();
            providerConfigMock.Setup(c => c.GetActiveMovieVideoProvider()).Returns(string.Empty);

            var selector = new MovieVideoServiceSelector(services, providerConfigMock.Object);

            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() => selector.GetService());
            Assert.Equal("Movie video provider is not defined", ex.Message);
        }

        [Fact]
        public void GetService_ThrowsArgumentException_WhenProviderNotFound()
        {
            // Arrange
            var serviceMock = new Mock<IMovieVideoService>();
            serviceMock.SetupGet(s => s.ProviderName).Returns("ProviderA");

            var services = new List<IMovieVideoService> { serviceMock.Object };

            var providerConfigMock = new Mock<IMovieProviderConfiguration>();
            providerConfigMock.Setup(c => c.GetActiveMovieVideoProvider()).Returns("NonExistentProvider");

            var selector = new MovieVideoServiceSelector(services, providerConfigMock.Object);

            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() => selector.GetService());
            Assert.Equal("Service 'NonExistentProvider' not found.", ex.Message);
        }
    }
}