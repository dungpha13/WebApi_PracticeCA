using MediatR;

namespace PracticeCA.Application;

[Authorize]
public class GetWeatherForecastQuery : IRequest<List<string>>, IQuery
{
}
