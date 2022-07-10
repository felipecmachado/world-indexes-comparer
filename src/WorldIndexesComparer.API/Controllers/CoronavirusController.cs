using MediatR;
using Microsoft.AspNetCore.Mvc;
using WorldIndexesComparer.Application.Queries;
using WorldIndexesComparer.Application.Results;

namespace WorldIndexesComparer.API.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/coronavirus")]
    public class CoronavirusController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CoronavirusController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet("summaries/{country}", Name = "GetCountrySummaryAsync")]
        [ProducesResponseType(typeof(SummaryByCountryResult), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCountrySummaryAsync(string country)
        {
            var response = await _mediator.Send(new GetSummaryByCountryQueryCommand(country));

            if (response is null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpGet("summary", Name = "GetGlobalSummaryAsync")]
        [ProducesResponseType(typeof(GlobalSummaryResult), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetGlobalSummaryAsync()
        {
            var response = await _mediator.Send(new GetGlobalSummaryQueryCommand());

            if (response is null)
            {
                return NotFound();
            }

            return Ok(response);
        }
    }
}