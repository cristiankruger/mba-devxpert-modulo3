namespace DevXpert.Modulo3.API.Configurations.App
{
    public interface IAppIdentityUser
    {
        string GetUsername();
        Guid GetUserId();
        bool IsAuthenticated();
        string GetUserRole();
        string GetRemoteIpAddress();
        string GetLocalIpAddress();
    }
}
