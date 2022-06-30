using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace ApiConventionAnalyser.Controllers
{
    [ApiController]
    //[ApiConventionType(typeof(DefaultApiConventions))]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };


        [HttpGet("GetComAnalyser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public ActionResult<IEnumerable<WeatherForecast>> GetComAnalyser()
        {
            var todos = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Id = index,
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();

            if (todos == null)
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpGet("GetComConvention")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public ActionResult<IEnumerable<WeatherForecast>> GetComConvention()
        {
            var todos =  Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Id = index, 
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();

            if (todos == null)
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpPost("PostComAnalyser")]
        [ProducesResponseType(typeof(WeatherForecast),StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]        
        public ActionResult PostComAnalyser( WeatherForecast weatherForecast)
        {
            if (weatherForecast == null)
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(PostComAnalyser), weatherForecast);
        }

        [HttpPost("PostComConvention")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        public ActionResult PostComConvention(WeatherForecast weatherForecast)
        {
            if (weatherForecast == null)
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(PostComConvention), weatherForecast);
        }

    }
}