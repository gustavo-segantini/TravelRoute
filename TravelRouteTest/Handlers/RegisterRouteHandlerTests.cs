using MediatR;
using Moq;
using TravelRouteApi.Commands;
using TravelRouteApi.Handlers;
using TravelRouteLib.Models;
using TravelRouteLib.Services;

namespace TravelRouteTest.Handlers
{
    [TestFixture]
    public class RegisterRouteHandlerTests
    {
        private Mock<IDijkstra> _mockDijkstra;
        private RegisterRouteHandler _handler;

        [SetUp]
        public void SetUp()
        {
            _mockDijkstra = new Mock<IDijkstra>();
            _handler = new RegisterRouteHandler(_mockDijkstra.Object);
        }

        [Test]
        public async Task Handle_ShouldCallAddRoute()
        {
            // Arrange
            var route = new RouteTrip("A", "B", 1);
            var command = new RegisterCommand(route);
            var cancellationToken = new CancellationToken();

            // Act
            await _handler.Handle(command, cancellationToken);

            // Assert
            _mockDijkstra.Verify(d => d.AddRoute(route), Times.Once);
        }

        [Test]
        public async Task Handle_ShouldReturnUnitValue()
        {
            // Arrange
            var route = new RouteTrip("A", "B", 1);
            var command = new RegisterCommand(route);
            var cancellationToken = new CancellationToken();

            // Act
            var result = await _handler.Handle(command, cancellationToken);

            // Assert
            Assert.That(result, Is.EqualTo(Unit.Value));
        }
    }
}
