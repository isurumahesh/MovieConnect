using Moq;
using MovieConnect.Application.Interfaces;
using MovieConnect.Application.Services;
using MovieConnect.Core.Interfaces;

namespace MovieConnect.UnitTests.Application
{
    public class MovieDetailServiceSelectorTests
    {
        [Fact]
        public void GetService_ReturnsCorrectService_WhenProviderIsDefined()
        {
            // Arrange
            var service1Mock = new Mock<IMovieDetailService>();
            service1Mock.SetupGet(s => s.ProviderName).Returns("ProviderA");

            var service2Mock = new Mock<IMovieDetailService>();
            service2Mock.SetupGet(s => s.ProviderName).Returns("ProviderB");

            var services = new List<IMovieDetailService> { service1Mock.Object, service2Mock.Object };

            var providerConfigMock = new Mock<IMovieProviderConfiguration>();
            providerConfigMock.Setup(c => c.GetActiveMovieDetailProvider()).Returns("ProviderB");

            var selector = new MovieDetailServiceSelector(services, providerConfigMock.Object);

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
            var services = new List<IMovieDetailService>();

            var providerConfigMock = new Mock<IMovieProviderConfiguration>();
            providerConfigMock.Setup(c => c.GetActiveMovieDetailProvider()).Returns(string.Empty);

            var selector = new MovieDetailServiceSelector(services, providerConfigMock.Object);

            // Act & Assert
            var ex = Assert.Throws<InvalidOperationException>(() => selector.GetService());
            Assert.Equal("Movie details provider is not defined", ex.Message);
        }

        [Fact]
        public void GetService_ThrowsArgumentException_WhenProviderNotFound()
        {
            // Arrange
            var serviceMock = new Mock<IMovieDetailService>();
            serviceMock.SetupGet(s => s.ProviderName).Returns("ProviderA");

            var services = new List<IMovieDetailService> { serviceMock.Object };

            var providerConfigMock = new Mock<IMovieProviderConfiguration>();
            providerConfigMock.Setup(c => c.GetActiveMovieDetailProvider()).Returns("NonExistentProvider");

            var selector = new MovieDetailServiceSelector(services, providerConfigMock.Object);

            // Act & Assert
            var ex = Assert.Throws<InvalidOperationException>(() => selector.GetService());
            Assert.Equal("Service 'NonExistentProvider' not found.", ex.Message);
        }
    }
}