﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using MinimalApiWithPowerAutomate.API.BusinessLayer.Services;
using MinimalApiWithPowerAutomate.API.Extensions;
using MinimalApiWithPowerAutomate.API.Registration;

namespace MinimalApiWithPowerAutomate.API.Handlers
{
    public class WeatherHandler : IRouteEndPointHandler
    {
        string ScopeRequiredByApi;

        public WeatherHandler(string scopeRequiredByApi)
        {
            ScopeRequiredByApi = scopeRequiredByApi;
        }

        public void Map(IEndpointRouteBuilder app)
        {
            app.MapGet("/weatherforecast", GetWeatherForecast)
            .WithName("GetWeatherForecast")
            .RequireAuthorization();
        }

        private async Task<IResult> GetWeatherForecast(
            [FromServices] WeatherService weatherService, 
            [FromServices] ILogger<WeatherHandler> logger,
            HttpContext httpContext)
        {
            if(!httpContext.VerifyUserHasAnyAcceptedScopeAndReturnRightResult(logger, out var userAccessResult, ScopeRequiredByApi))
                return userAccessResult;

            var result = await weatherService.GetWeatherForecastAsync();

            return Results.Ok(result);
        }
    }
}
