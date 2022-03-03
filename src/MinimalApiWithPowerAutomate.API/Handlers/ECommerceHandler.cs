using Microsoft.Identity.Web.Resource;
using MinimalApiWithPowerAutomate.API.Registration;

namespace MinimalApiWithPowerAutomate.API.Handlers
{
    public class ECommerceHandler : IRouteEndPointHandler
    {
        public void Map(IEndpointRouteBuilder app, string scopeRequiredByApi)
        {
            app.MapGet("/orders", (HttpContext httpContext) =>
            {
                //httpContext.VerifyUserHasAnyAcceptedScope(scopeRequiredByApi);

                return String.Empty;
            })
            .WithName("GetWeatherForecast")
            ;//.RequireAuthorization();
        }
    }
}
