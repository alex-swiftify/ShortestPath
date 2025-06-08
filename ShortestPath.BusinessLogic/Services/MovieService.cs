using System.Text.Json;
using ShortestPath.BusinessLogic.Entities;

namespace ShortestPath.BusinessLogic.Services;

public class MovieService
{
    private List<Movie> _movies = [];
    private List<Actor> _actors = [];

    public void LoadMovies()
    {
        string jsonFilePath = Path.Combine(AppContext.BaseDirectory, "Resources", "movies.json");
        string json = File.ReadAllText(jsonFilePath);
        
        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        MoviesData? moviesData = JsonSerializer.Deserialize<MoviesData>(json, options);
        
        if (moviesData == null)
            throw new InvalidOperationException("Deserialization resulted in null movies data.");
        
        _movies = moviesData.Movies;
        
        _actors = _movies
            .SelectMany(m => m.Cast)
            .Select(c => new Actor { Name = c, Movies = _movies.Where(m => m.Cast.Contains(c)).Select(m => m.Title).ToList() })
            .ToList();
    }
    
    private Movie? FindCommonMovie(string fromActor, string toActor) =>
        _movies.FirstOrDefault(movie =>
                movie.Cast.Contains(fromActor) &&
                movie.Cast.Contains(toActor));

    public List<string> FindShortestPath(string fromActor, string toActor)
    {
        return FindShortestPath(fromActor, toActor, []);
    }
    
    private List<string> FindShortestPath(string fromActor, string toActor, HashSet<string> visited)
    {
        if (fromActor == toActor)
            return [toActor];
        
        Movie? commonMovie = FindCommonMovie(fromActor, toActor);
        
        if (commonMovie != null)
            return [fromActor, commonMovie.Title, toActor];
        
        IEnumerable<string> coActors = _movies
            .Where(m => m.Cast.Contains(fromActor))
            .SelectMany(m => m.Cast)
            .Except([fromActor]);

        List<string> shortestPath = [];

        foreach (string coActor in coActors.Except(visited))
        {
            commonMovie = FindCommonMovie(fromActor, coActor);
            if (commonMovie is not {} movie)
                throw new InvalidOperationException();
                
            if (visited.Contains(commonMovie.Title))
                continue;
                
            List<string> path = FindShortestPath(coActor, toActor, [fromActor, movie.Title]);
            
            if (path.Any() &&
                (!shortestPath.Any() || path.Count < shortestPath.Count))
            {
                shortestPath = path;
            }
        }
        
        if (shortestPath.Any())
        {
            string to = shortestPath.First();
            Movie? common =  FindCommonMovie(fromActor, to);
            
            if (common is not {} movie)
                throw new InvalidOperationException();
                            
            shortestPath.Insert(0, fromActor);
            shortestPath.Insert(1, movie.Title);
        }
        
        return shortestPath; 
    }
}