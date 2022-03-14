using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Data;

namespace Api;

public class MoviesPost
{
    private readonly IMovieData movieData;

    public MoviesPost(IMovieData movieData)
    {
        this.movieData = movieData;
    }

    [FunctionName("MoviesPost")]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "movies")] HttpRequest req,
        ILogger log)
    {
        var body = await new StreamReader(req.Body).ReadToEndAsync();
        var movie = JsonSerializer.Deserialize<Movie>(body, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

        var newMovie = await movieData.AddMovie(movie);
        return new OkObjectResult(newMovie);
    }
}
