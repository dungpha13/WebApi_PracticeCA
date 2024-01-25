using MediatR;

namespace PracticeCA.Application.WeatherForecast.GetWeatherForecast;

public class GetWeatherForecastQueryHandler : IRequestHandler<GetWeatherForecastQuery, List<string>>
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    public async Task<List<string>> Handle(GetWeatherForecastQuery request, CancellationToken cancellationToken)
    {
        await Task.Delay(1000);
        var result = Summaries.ToList();
        return result;
    }
}