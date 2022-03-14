using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;

namespace Api;

public interface IMovieData
{
    Task<Movie> AddMovie(Movie movie);
    Task<bool> DeleteMovie(int id);
    Task<IEnumerable<Movie>> GetMovies();
    Task<Movie> UpdateMovie(Movie movie);
}

public class MovieData : IMovieData
{
    private readonly List<Movie> movies = new List<Movie>
        {
            new Movie
            {
                Id = 10,
                Title = "the Batman",
                Description = "1980's movie, Batman",
                Quantity = 1
            },
            new Movie
            {
                Id = 20,
                Title = "The Shooter",
                Description = "Classic sniper, shoot em' up!",
                Quantity = 1
            },
            new Movie
            {
                Id = 30,
                Title = "Ex Machina",
                Description = "This one makes you think!",
                Quantity = 1
            }
        };

    private int GetRandomInt()
    {
        var random = new Random();
        return random.Next(100, 1000);
    }

    public Task<Movie> AddMovie(Movie movie)
    {
        movie.Id = GetRandomInt();
        movies.Add(movie);
        return Task.FromResult(movie);
    }

    public Task<Movie> UpdateMovie(Movie movie)
    {
        var index = movies.FindIndex(p => p.Id == movie.Id);
        movies[index] = movie;
        return Task.FromResult(movie);
    }

    public Task<bool> DeleteMovie(int id)
    {
        var index = movies.FindIndex(p => p.Id == id);
        movies.RemoveAt(index);
        return Task.FromResult(true);
    }

    public Task<IEnumerable<Movie>> GetMovies()
    {
        return Task.FromResult(movies.AsEnumerable());
    }
}
