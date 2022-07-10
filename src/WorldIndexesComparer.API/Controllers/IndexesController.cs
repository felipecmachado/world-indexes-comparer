using Microsoft.AspNetCore.Mvc;

namespace WorldIndexesComparer.API.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/indexes")]
    public class IndexesController : ControllerBase
    {
        public IndexesController()
        {
        }

        [HttpGet("cpi/{country}", Name = "GetCountryConsumerPriceIndexAsync")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public Task<string> GetCountryConsumerPriceIndexAsync(string country)
        {
            throw new NotImplementedException();
        }
    }
}