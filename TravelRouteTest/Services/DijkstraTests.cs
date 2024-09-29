using Microsoft.Extensions.Options;
using Moq;
using TravelRouteLib.Configuration;
using TravelRouteLib.Models;
using TravelRouteLib.Services;

namespace TravelRouteTest.Services
{
    [TestFixture]
    public class DijkstraTests
    {
        private Mock<IOptions<PathCsvFile>> _mockConfig;

        private Dijkstra _dijkstra;

        private const string TestFilePath = "test_routes.csv";

        [SetUp]
        public void SetUp()
        {
            File.WriteAllText(TestFilePath, $"A,B,1{Environment.NewLine}B,C,2{Environment.NewLine}A,C,4{Environment.NewLine}C,D,4");

            _mockConfig = new Mock<IOptions<PathCsvFile>>();
            _mockConfig.Setup(c => c.Value).Returns(new PathCsvFile { Path = TestFilePath });

            _dijkstra = new Dijkstra(_mockConfig.Object);
            _dijkstra.LoadRoutes();
        }

        [TearDown]
        public void TearDown()
        {
            if (File.Exists(TestFilePath))
            {
                File.Delete(TestFilePath);
            }

            _dijkstra.Dispose();
        }

        [Test]
        public Task LoadRoutes_ShouldLoadRoutesFromFile()
        {
            Assert.That(Dijkstra.Graph.ContainsKey("A"), Is.True);
            Assert.That(Dijkstra.Graph.ContainsKey("B"), Is.True);
            Assert.That(Dijkstra.Graph["A"].Count, Is.EqualTo(2));
            Assert.That(Dijkstra.Graph["B"].Count, Is.EqualTo(1));
            return Task.CompletedTask;
        }

        [Test]
        public async Task AddRoute_ShouldAddRouteToGraphAndFile()
        {
            var newRoute = new RouteTrip("C", "D", 3);
            await _dijkstra.AddRoute(newRoute);

            Assert.That(Dijkstra.Graph.ContainsKey("C"), Is.True);
            Assert.That(Dijkstra.Graph["C"].Count, Is.EqualTo(2));
            Assert.That(Dijkstra.Graph["C"][0], Is.EqualTo(("D", 4)));

            var fileContent = await File.ReadAllTextAsync(TestFilePath);
            Assert.That(fileContent.Contains("C,D,3"), Is.True);
        }

        [Test]
        public async Task FindBestRoute_ShouldReturnBestRoute()
        {
            var bestRoute = await _dijkstra.FindBestRoute("A", "C");

            Assert.That(bestRoute.Route, Is.EqualTo("A - B - C"));
            Assert.That(bestRoute.Cost, Is.EqualTo(3));
        }

        [Test]
        public async Task FindBestRoute_ShouldReturnNoRouteFound_WhenNoRouteExists()
        {
            var bestRoute = await _dijkstra.FindBestRoute("A", "D");

            Assert.That(bestRoute.Route, Is.EqualTo("A - B - C - D"));
            Assert.That(bestRoute.Cost, Is.EqualTo(7));
        }
    }
}
