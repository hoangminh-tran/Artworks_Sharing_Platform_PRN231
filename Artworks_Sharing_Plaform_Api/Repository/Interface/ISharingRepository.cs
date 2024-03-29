using Artworks_Sharing_Plaform_Api.Model;

namespace Artworks_Sharing_Plaform_Api.Repository.Interface
{
    public interface ISharingRepository
    {
        Task<bool> CreateSharingPostArtwork(Sharing sharing);
    }
}
