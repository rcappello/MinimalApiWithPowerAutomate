using Microsoft.Identity.Web.Resource;
using MinimalApiWithPowerAutomate.API.Registration;

namespace MinimalApiWithPowerAutomate.API.Handlers
{
    public class WeatherHandler : IRouteEndPointHandler
    {
        public void Map(IEndpointRouteBuilder app, string scopeRequiredByApi)
        {
            var summaries = new[]
            {
                "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
            };

            app.MapGet("/weatherforecast", (HttpContext httpContext) =>
            {
                httpContext.VerifyUserHasAnyAcceptedScope(scopeRequiredByApi);

                var forecast = Enumerable.Range(1, 5).Select(index =>
                   new WeatherForecast
                   (
                       DateTime.Now.AddDays(index),
                       Random.Shared.Next(-20, 55),
                       summaries[Random.Shared.Next(summaries.Length)]
                   ))
                    .ToArray();
                return forecast;
            })
            .WithName("GetWeatherForecast")
            .RequireAuthorization();
        }

        internal record WeatherForecast(DateTime Date, int TemperatureC, string? Summary)
        {
            public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
        }
    }
}
