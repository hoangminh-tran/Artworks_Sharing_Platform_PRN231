using Artworks_Sharing_Plaform_Api.Model;

namespace Artworks_Sharing_Plaform_Api.Service.Interface
{
    public interface IStatusService
    {
        Task<Status?> GetStatusByStatusName(string statusName);
    }
}
