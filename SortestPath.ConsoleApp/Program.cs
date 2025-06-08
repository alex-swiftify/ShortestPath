using ShortestPath.BusinessLogic.Services;

var service = new MovieService();
service.LoadMovies();

List<string> shortestPath = service.FindShortestPath("Javier Bardem", "Tom Cruise");

Console.WriteLine($"Shortest path: {string.Join(" => ", shortestPath)}");
