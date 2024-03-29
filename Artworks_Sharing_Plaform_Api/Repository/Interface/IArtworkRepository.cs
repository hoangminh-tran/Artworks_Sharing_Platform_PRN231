using Artworks_Sharing_Plaform_Api.Model;

namespace Artworks_Sharing_Plaform_Api.Repository.Interface
{
    public interface IArtworkRepository
    {
        Task<Artwork> CreateArtworkAsync(Artwork artwork);
        Task<Artwork?> GetArtworkByIdAsync(Guid artworkId);
        Task<Artwork?> GetArtworkByArtworkByIdAsync(Guid artworkId);
        Task<List<Artwork>> GetListArtworkByCreatorIdAsync(Guid creatorId);
        Task<List<Artwork>> GetListArtworkByUserIdAsync(Guid userId);
        Task<Artwork?> GetArtworkByOrderIdAsync(Guid orderId);
        Task<bool> UpdateArtworkAsync(Artwork artwork);
        Task<List<Artwork>> GetListArtworkOwnByUserAsync(Guid userId);
        Task<List<Artwork>> GetListArtworkAsync();
        Task<List<Artwork>> GetListArtworkByArtworkNameAsync(string? artworkName, Guid statusId);
        Task<List<Artwork>> GetListArtworkByCreatorIdAndStatusAsync(Guid creatorId, Guid statusId);
        Task<List<Artwork>> GetListArtworkByFilterListTypeOfArtworkAndArtistAsync(List<Guid>? typeOfArtworkIds, Guid? artistId, Guid statusId);
    }
}
