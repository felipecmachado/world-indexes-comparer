using Microsoft.AspNetCore.Mvc;

namespace WorldIndexesComparer.API.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class CoronavirusController : ControllerBase
    {
        public CoronavirusController()
        {
        }

        [HttpGet("summaries/{country}", Name = "GetCountrySummaryAsync")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public Task<string> GetCountrySummaryAsync(string country)
        {
            throw new NotImplementedException();
        }

        [HttpGet("history/{country}", Name = "GetCountryHistoryAsync")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public Task<string> GetCountryHistoryAsync(string country)
        {
            throw new NotImplementedException();
        }

        [HttpGet("history", Name = "GetGlobalHistoryAsync")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public Task<string> GetGlobalHistoryAsync()
        {
            throw new NotImplementedException();
        }
    }
}