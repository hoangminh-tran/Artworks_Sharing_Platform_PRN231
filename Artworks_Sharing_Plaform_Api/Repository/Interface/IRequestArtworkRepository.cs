using Artworks_Sharing_Plaform_Api.Model;

namespace Artworks_Sharing_Plaform_Api.Repository.Interface
{
    public interface IRequestArtworkRepository
    {
        public Task<bool> AcceptOrRejectRequestArtwork(RequestArtwork requestArtwork);
        public Task<RequestArtwork?> GetRequestArtworkByRequestArtworkId(Guid requestArtworkId);
        Task<bool> CreateRequestArtworkAsync(RequestArtwork requestArtwork);
        Task<List<RequestArtwork>> GetListRequestArtworkByBookingIdAsync(Guid bookingId);
        Task<bool> UpdateRequestArtworkAsync(RequestArtwork requestArtwork);
    }
}
