using Microsoft.AspNetCore.Mvc;

namespace WorldIndexesComparer.API.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class StatsController : ControllerBase
    {
        public StatsController()
        {
        }

        [HttpGet("{country}", Name = "GetCountryStatsAsync")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public Task<string> GetCountryStatsAsync(string country)
        {
            throw new NotImplementedException();
        }
    }
}