using Microsoft.Identity.Web.Resource;

namespace MinimalApiWithPowerAutomate.API.Extensions
{
    public static class AccessExtension
    {
        public static bool VerifyUserHasAnyAcceptedScopeAndReturnRightResult(this HttpContext httpContext, 
            ILogger logger, 
            out IResult result, params string[] acceptedScopes)
        {
            logger.LogInformation("weatherforecast accessed by user: {Name}", httpContext.User.Identity?.Name);
            logger.LogInformation("VerifyUserHasAnyAcceptedScope: {acceptedScopes}", acceptedScopes);

            try
            {
                httpContext.VerifyUserHasAnyAcceptedScope(acceptedScopes);
            }
            catch (ArgumentNullException)
            {
                result = Results.StatusCode(500);
                return false;
            }
            catch (UnauthorizedAccessException)
            {
                result = Results.Unauthorized();
                return false; 
            }
            result = Results.Ok();
            return true;
        }
    }
}
