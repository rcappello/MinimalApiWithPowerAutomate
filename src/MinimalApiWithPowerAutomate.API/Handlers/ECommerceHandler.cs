using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using MinimalApiWithPowerAutomate.API.BusinessLayer.Services;
using MinimalApiWithPowerAutomate.API.Extensions;
using MinimalApiWithPowerAutomate.API.Registration;
using MinimalApiWithPowerAutomate.API.Shared.Models.Responses;

namespace MinimalApiWithPowerAutomate.API.Handlers
{
    public class ECommerceHandler : IRouteEndPointHandler
    {
        readonly string ScopeRequiredByApi;

        public ECommerceHandler(string scopeRequiredByApi)
        {
            ScopeRequiredByApi = scopeRequiredByApi;
        }

        public void Map(IEndpointRouteBuilder app)
        {
            app.MapGet("/orders", GetOrdersAsync)
            .WithName("GetOrders")
            .Produces(StatusCodes.Status200OK, typeof(ListResult<Order>))
            .Produces(StatusCodes.Status401Unauthorized)
            .RequireAuthorization();

            app.MapGet("/customer/{id:int}", GetCustomerAsync)
            .WithName("GetCustomer")
            .Produces(StatusCodes.Status200OK, typeof(Customer))
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status404NotFound)
            .RequireAuthorization();
        }

        private async Task<IResult> GetOrdersAsync(
            [FromServices] ECommerceService ecommerceService,
            [FromServices] ILogger<ECommerceHandler> logger,
            HttpContext httpContext,
            [FromQuery(Name = "q")] string searchText = null,
            [FromQuery(Name = "page")] int pageIndex = 0,
            [FromQuery(Name = "size")] int itemsPerPage = 20
            )
        {
            if (!httpContext.VerifyUserHasAnyAcceptedScopeAndReturnRightResult(logger, out var userAccessResult, ScopeRequiredByApi))
                return userAccessResult;

            var result = await ecommerceService.GetOrdersAsync(searchText, pageIndex, itemsPerPage);

            return Results.Ok(result);
        }

        private async Task<IResult> GetCustomerAsync(
            int id,
            [FromServices] ECommerceService ecommerceService,
            [FromServices] ILogger<ECommerceHandler> logger,
            HttpContext httpContext
            )
        {
            if (!httpContext.VerifyUserHasAnyAcceptedScopeAndReturnRightResult(logger, out var userAccessResult, ScopeRequiredByApi))
                return userAccessResult;

            var result = await ecommerceService.GetCustomerByIDAsync(id);

            if (result == null) return Results.NotFound();
            return Results.Ok(result);
        }
    }
}
