using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using MinimalApiWithPowerAutomate.API.BusinessLayer.Services.Base;
using MinimalApiWithPowerAutomate.API.DataAccessLayer;
using MinimalApiWithPowerAutomate.API.Shared.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinimalApiWithPowerAutomate.API.BusinessLayer.Services
{
    public class WeatherService : BaseService, IWeatherService
    {
        public WeatherService(IApplicationDbContext dataContext,
                                IHttpContextAccessor httpContextAccessor, 
                                ILogger<WeatherService> logger, 
                                IMapper mapper)
            : base(dataContext, httpContextAccessor, logger, mapper)
        {
        }

        public async Task<WeatherForecast[]> GetWeatherForecastAsync()
        {
            var summaries = new[]
            {
                "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
            };

            var forecast = Enumerable.Range(1, 5).Select(index =>
                new WeatherForecast
                (
                    DateTime.Now.AddDays(index),
                    Random.Shared.Next(-20, 55),
                    summaries[Random.Shared.Next(summaries.Length)]
                ))
                .ToArray();
            return forecast;
        }
    }
}