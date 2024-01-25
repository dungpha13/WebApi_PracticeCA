using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace PracticeCA.Application.WeatherForecast.GetWeatherForecast
{
    public class GetWeatherForeCastQueryValidator : AbstractValidator<GetWeatherForecastQuery>
    {
        public GetWeatherForeCastQueryValidator()
        {
        }
    }
}