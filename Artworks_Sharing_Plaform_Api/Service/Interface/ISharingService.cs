using Artworks_Sharing_Plaform_Api.Model;
using Artworks_Sharing_Plaform_Api.Model.Dto.ReqDto;

namespace Artworks_Sharing_Plaform_Api.Service.Interface
{
    public interface ISharingService
    {
        Task<bool> CreateSharingPostArtwork(SharePostArtworkDto sharingPostDto);
    }
}
