using MediatR;

namespace PracticeCA.Application;

[Authorize(Policy = "EmployeeOnly")]
public class GetWeatherForecastQuery : IRequest<List<string>>, IQuery
{
}
