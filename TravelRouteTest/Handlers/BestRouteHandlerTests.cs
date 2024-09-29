using Moq;
using TravelRouteApi.Commands;
using TravelRouteApi.Handlers;
using TravelRouteLib.Models;
using TravelRouteLib.Services;

namespace TravelRouteTest.Handlers
{
    [TestFixture]
    public class BestRouteHandlerTests
    {
        [Test]
        public async Task Handle_ReturnsBestRoute()
        {
            // Arrange
            var mockDijkstra = new Mock<IDijkstra>();
            var bestRoute = new BestRoute { Route = "GRU", Cost = 10 };
            mockDijkstra.Setup(d => d.FindBestRoute(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(bestRoute);

            var handler = new BestRouteHandler(mockDijkstra.Object);
            var request = new BestRouteQuery { From = "A", To = "B" };
            var cancellationToken = new CancellationToken();

            // Act
            var result = await handler.Handle(request, cancellationToken);

            // Assert
            Assert.That(result, Is.EqualTo(bestRoute));
        }

        [Test]
        public async Task Handle_CallsFindBestRouteWithCorrectParameters()
        {
            // Arrange
            var mockDijkstra = new Mock<IDijkstra>();
            var handler = new BestRouteHandler(mockDijkstra.Object);
            var request = new BestRouteQuery { From = "A", To = "B" };
            var cancellationToken = new CancellationToken();

            // Act
            await handler.Handle(request, cancellationToken);

            // Assert
            mockDijkstra.Verify(d => d.FindBestRoute("A", "B"), Times.Once);
        }
    }
}
