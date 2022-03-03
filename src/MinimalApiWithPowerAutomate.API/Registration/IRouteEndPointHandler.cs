namespace MinimalApiWithPowerAutomate.API.Registration;

public interface IRouteEndPointHandler
{
    public void Map(IEndpointRouteBuilder app, string scopeRequiredByApi);
}