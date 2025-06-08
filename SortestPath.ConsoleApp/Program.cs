using ShortestPath.BusinessLogic.Services;

var service = new MovieService();
service.LoadMovies();

// foreach (var movie in service._movies)
// {
//     Console.WriteLine($"{movie.Title}: {string.Join(", ", movie.Cast.Select(a => a.Name))}");
// }

List<string> shortestPath = service.FindShortestPath("Javier Bardem", "Tom Cruise");

Console.WriteLine($"Shortest path: {string.Join(" => ", shortestPath)}");
