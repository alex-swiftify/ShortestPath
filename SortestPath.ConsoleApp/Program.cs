using ShortestPath.BusinessLogic.Models.Entities;
using ShortestPath.BusinessLogic.Services;

var service = new MovieService();
service.LoadMovies();

IEnumerable<IPathItem>? shortestPath = service.FindShortestPath("Javier Bardem", "Tom Cruise");

Console.WriteLine(
    shortestPath is null
        ? "No shortest path found."
        : $"Shortest path: {string.Join(" => ", shortestPath.Select(item => item.DisplayName))}."
);
