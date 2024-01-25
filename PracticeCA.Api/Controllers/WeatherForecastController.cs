using MediatR;
using Microsoft.AspNetCore.Mvc;
using PracticeCA.Application;

namespace PracticeCA.Api.Controllers;


[ApiController]
[Route("[controller]")]
[Authorize]
public class WeatherForecastController : ControllerBase
{
    private readonly ISender _mediator;

    public WeatherForecastController(ISender mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpGet]
    public async Task<ActionResult> GetWeatherForecast()
    {
        var query = new GetWeatherForecastQuery();
        var result = await _mediator.Send(query, default);
        return Ok(result);
    }
}
