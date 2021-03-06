using System.Reflection;

namespace MinimalApiWithPowerAutomate.API.Registration;

public static class WebApplicationExtensions
{
    public static void RegisterEndpoints(this WebApplication app, Assembly assembly, string scopeRequiredByApi)
    {
        var routeEndpointHandlerInterfaceType = typeof(IRouteEndPointHandler);

        var routeEndpointHanlderTypes = assembly.GetTypes().Where(t =>
            t.IsClass && !t.IsAbstract
            && routeEndpointHandlerInterfaceType.IsAssignableFrom(t));

        foreach (var routeEndpointHandlerType in routeEndpointHanlderTypes)
        {
            var instantiatedType = (IRouteEndPointHandler) Activator.CreateInstance(routeEndpointHandlerType, scopeRequiredByApi );
            instantiatedType.Map(app);
        }
    }
}