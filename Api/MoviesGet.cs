using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using System.Threading.Tasks;

namespace Api;

public class MoviesGet
{
    private readonly IMovieData movieData;

    public MoviesGet(IMovieData movieData)
    {
        this.movieData = movieData;
    }

    [FunctionName("MoviesGet")]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "movies")] HttpRequest req)
    {
        var movies = await movieData.GetMovies();
        return new OkObjectResult(movies);
    }
}