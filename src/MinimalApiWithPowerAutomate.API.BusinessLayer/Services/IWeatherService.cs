using MinimalApiWithPowerAutomate.API.Shared.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinimalApiWithPowerAutomate.API.BusinessLayer.Services
{
    public interface IWeatherService
    {
        Task<WeatherForecast[]> GetWeatherForecastAsync();
    }
}
