using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;

namespace DevXpert.Modulo3.API.Configurations.Extensions;

[ExcludeFromCodeCoverage]
public class CustomAuthorization
{
    public static bool ValidarClaimsUsuario(HttpContext context, string claimName, string claimValue)
    {
        return context.User.Identity.IsAuthenticated &&
               context.User.Claims.Any(c => c.Type == claimName && c.Value.Contains(claimValue));
    }
}

[ExcludeFromCodeCoverage]
public class ClaimsAuthorizeAttribute : TypeFilterAttribute
{
    public ClaimsAuthorizeAttribute(string claimName, string claimValue) : base(typeof(RequisitoClaimFilter))
    {
        Arguments = [new Claim(claimName, claimValue)];
    }
}

[ExcludeFromCodeCoverage]
public class RequisitoClaimFilter(Claim claim) : IAuthorizationFilter
{
    private readonly Claim _claim = claim;

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if (!context.HttpContext.User.Identity.IsAuthenticated)
        {
            context.Result = new StatusCodeResult(401);
            return;
        }

        if (!CustomAuthorization.ValidarClaimsUsuario(context.HttpContext, _claim.Type, _claim.Value))
            context.Result = new StatusCodeResult(403);
    }
}
