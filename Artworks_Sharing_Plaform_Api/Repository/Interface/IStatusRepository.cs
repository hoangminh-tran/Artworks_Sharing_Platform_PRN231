using Artworks_Sharing_Plaform_Api.Model;

namespace Artworks_Sharing_Plaform_Api.Repository.Interface
{
    public interface IStatusRepository
    {
        Task<Status?> GetStatusByNameAsync(string name);
        Task<Status?> GetStatusByStatusIDAsync(Guid StatusID);
    }
}
