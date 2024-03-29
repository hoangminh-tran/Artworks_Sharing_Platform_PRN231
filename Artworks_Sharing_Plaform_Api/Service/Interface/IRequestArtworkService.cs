using Artworks_Sharing_Plaform_Api.Model;

namespace Artworks_Sharing_Plaform_Api.Service.Interface
{
    public interface IRequestArtworkService
    {
        public Task<string> AcceptOrRejectRequestArtwork(bool isAccept, Guid requestArtworkId);
        public Task<RequestArtwork> GetRequestArtworkByRequestArtworkId(Guid requestArtworkId);
    }
}
