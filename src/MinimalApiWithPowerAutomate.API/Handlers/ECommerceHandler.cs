using Microsoft.Identity.Web.Resource;
using MinimalApiWithPowerAutomate.API.Registration;

namespace MinimalApiWithPowerAutomate.API.Handlers
{
    public class ECommerceHandler : IRouteEndPointHandler
    {
        string ScopeRequiredByApi;

        public ECommerceHandler(string scopeRequiredByApi)
        {
            ScopeRequiredByApi = scopeRequiredByApi;
        }

        public void Map(IEndpointRouteBuilder app)
        {
            app.MapGet("/orders", (HttpContext httpContext) =>
            {
                //httpContext.VerifyUserHasAnyAcceptedScope(scopeRequiredByApi);

                return String.Empty;
            })
            .WithName("GetOrders")
            ;//.RequireAuthorization();
        }
    }
}
