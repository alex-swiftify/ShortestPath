using ShortestPath.BusinessLogic.Entities.Interfaces;
using ShortestPath.BusinessLogic.Services;

var service = new MovieService();
service.LoadMovies();

// foreach (var movie in service._movies)
// {
//     Console.WriteLine($"{movie.Title}: {string.Join(", ", movie.Cast.Select(a => a.Name))}");
// }

List<IPathItem>? shortestPath = service.FindShortestPath("Javier Bardem", "Tom Cruise");

Console.WriteLine(
    shortestPath is null
        ? "No shortest path found."
        : $"Shortest path: {string.Join(" => ", shortestPath.Select(item => item.DisplayName))}."
);
