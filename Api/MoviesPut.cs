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

public class MoviesPut
{
    private readonly IMovieData movieData;

    public MoviesPut(IMovieData movieData)
    {
        this.movieData = movieData;
    }

    [FunctionName("MoviesPut")]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "movies")] HttpRequest req,
        ILogger log)
    {
        var body = await new StreamReader(req.Body).ReadToEndAsync();
        var movie = JsonSerializer.Deserialize<Movie>(body, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

        var updatedMovie = await movieData.UpdateMovie(movie);
        return new OkObjectResult(updatedMovie);
    }
}
