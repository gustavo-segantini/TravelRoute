using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TravelRouteApi.Commands;
using TravelRouteApi.Controllers;
using TravelRouteLib.Models;

namespace TravelRouteTest.Controllers
{
    [TestFixture]
    public class RoutesControllerTests
    {
        private Mock<IMediator> _mediatorMock;


        private RoutesController _controller;

        [SetUp]
        public void SetUp()
        {
            _mediatorMock = new Mock<IMediator>();
            _controller = new RoutesController(_mediatorMock.Object);
        }

        [Test]
        public async Task RegisterRoute_ValidRoute_ReturnsOk()
        {
            // Arrange
            var route = new RouteTrip("A", "B", 2);
            _mediatorMock.Setup(m => m.Send(It.IsAny<RegisterCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Unit.Value);

            // Act
            var result = await _controller.RegisterRoute(route);

            // Assert
            Assert.That(result.StatusCode, Is.EqualTo(StatusCodes.Status200OK));
        }

        [Test]
        public async Task GetBestRoute_ValidParameters_ReturnsOkWithBestRoute()
        {
            // Arrange
            const string from = "A";
            const string to = "B";
            var bestRoute = new BestRoute { Route = "A-B", Cost = 100 };
            _mediatorMock.Setup(m => m.Send(It.IsAny<BestRouteQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(bestRoute);

            // Act
            var result = await _controller.GetBestRoute(from, to) as OkObjectResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Value, Is.Not.Null);
            Assert.That(result.StatusCode, Is.EqualTo(StatusCodes.Status200OK));
        }

        [Test]
        public async Task GetBestRoute_MissingParameters_ReturnsBadRequest()
        {
            // Act
            var result = await _controller.GetBestRoute(string.Empty, string.Empty) as BadRequestObjectResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Value, Is.EqualTo("From and To are required"));
            Assert.That(result.StatusCode, Is.EqualTo(StatusCodes.Status400BadRequest));
        }

        [TearDown]
        public void TearDown()
        {
            _controller.Dispose();
        }
    }
}
