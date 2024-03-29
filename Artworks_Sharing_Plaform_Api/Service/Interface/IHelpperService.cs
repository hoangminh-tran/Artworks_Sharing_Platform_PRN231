namespace Artworks_Sharing_Plaform_Api.Service.Interface
{
    public interface IHelpperService
    {
        bool CheckBearerTokenIsValidAndNotExpired(string token);
        Guid GetAccIdFromLogged();
        bool IsTokenValid();
        Guid GetAccIdFromLoogedNotThrow();
    }
}
