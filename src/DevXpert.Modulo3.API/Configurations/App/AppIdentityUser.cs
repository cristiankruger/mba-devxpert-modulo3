using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;

namespace DevXpert.Modulo3.API.Configurations.App;

public class AppIdentityUser(IHttpContextAccessor accessor) : IAppIdentityUser
{
    public string GetLocalIpAddress()
    {
        return accessor.HttpContext?.Connection.LocalIpAddress?.ToString();
    }

    public string GetRemoteIpAddress()
    {
        return accessor.HttpContext?.Connection.RemoteIpAddress?.ToString();
    }

    public Guid GetUserId()
    {
        return IsAuthenticated() ? Guid.Parse(accessor.HttpContext.User.GetUserId()) : Guid.Empty;
    }

    public string GetUsername()
    {
        return accessor.HttpContext.User.Identity.Name;
    }

    public bool IsAuthenticated()
    {
        return accessor.HttpContext?.User.Identity is { IsAuthenticated: true };
    }

    public string GetUserRole()
    {
        return IsAuthenticated() ? accessor.HttpContext.User.GetUserRole() : string.Empty;
    }
}

[ExcludeFromCodeCoverage]
public static class ClaimsPrincipalExtensions
{
    public static string GetUserId(this ClaimsPrincipal principal)
    {
        if (principal is null)
            throw new ArgumentException(null, nameof(principal));

        var claim = principal.FindFirst(ClaimTypes.NameIdentifier);
        return claim?.Value;
    }

    public static string GetUserEmail(this ClaimsPrincipal principal)
    {
        if (principal is null)
            throw new ArgumentException(null, nameof(principal));

        var claim = principal.FindFirst(ClaimTypes.Email);
        return claim?.Value;
    }

    public static string GetUserRole(this ClaimsPrincipal principal)
    {
        if (principal is null)
            throw new ArgumentException(null, nameof(principal));

        var claim = principal.FindFirst(ClaimTypes.Role);
        return claim?.Value;
    }
}