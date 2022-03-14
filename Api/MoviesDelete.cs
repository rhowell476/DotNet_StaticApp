using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Api;

public class MoviesDelete
{
    private readonly IMovieData movieData;

    public MoviesDelete(IMovieData movieData)
    {
        this.movieData = movieData;
    }

    [FunctionName("MoviesDelete")]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "movies/{movieId:int}")] HttpRequest req,
        int movieId,
        ILogger log)
    {
        var result = await movieData.DeleteMovie(movieId);

        if (result)
        {
            return new OkResult();
        }
        else
        {
            return new BadRequestResult();
        }
    }
}
