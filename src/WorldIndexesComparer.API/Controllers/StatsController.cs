using MediatR;
using Microsoft.AspNetCore.Mvc;
using WorldIndexesComparer.Application.Queries;
using WorldIndexesComparer.Application.Results;

namespace WorldIndexesComparer.API.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/stats")]
    public class StatsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StatsController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet("comparison", Name = "GetCountriesComparisonAsync")]
        [ProducesResponseType(typeof(CountriesComparisonResult), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCountriesComparisonAsync([FromQuery] string[] countries)
        {
            if (countries.Length == 0)
            {
                return NoContent();
            }

            var response = await _mediator.Send(new GetCountriesComparisonQueryCommand(countries));

            if (response is null)
            {
                return NotFound();
            }

            return Ok(response);
        }
    }
}