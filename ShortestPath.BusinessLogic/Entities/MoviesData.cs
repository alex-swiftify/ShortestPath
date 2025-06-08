namespace ShortestPath.BusinessLogic.Entities;

public class MoviesData(List<Movie> movies)
{
    public List<Movie> Movies { get; init; } = movies;
}