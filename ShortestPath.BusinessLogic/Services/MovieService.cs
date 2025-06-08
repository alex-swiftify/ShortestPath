using System.Text.Json;
using ShortestPath.BusinessLogic.Entities;

namespace ShortestPath.BusinessLogic.Services;

public class MovieService
{
    private List<Movie> _movies = [];

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
    }
    
    public List<string> FindShortestPath(string fromActor, string toActor)
    {
        return FindShortestPath(fromActor, toActor, []);
    }
    
    private List<string> FindShortestPath(string fromActor, string toActor, HashSet<string> visited)
    {
        if (fromActor == toActor)
            return [toActor];
        
        return []; 
    }
}