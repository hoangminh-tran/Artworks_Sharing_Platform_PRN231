using Artworks_Sharing_Plaform_Api.Model;

namespace Artworks_Sharing_Plaform_Api.Repository.Interface
{
    public interface IArtworkTypeRepository
    {
        Task<bool> CreateArtworkTypeAsync(ArtworkType artworkType);
        Task<bool> UpdateArtworkTypeAsync(ArtworkType update);
        Task<List<ArtworkType>> GetAllArtworkTypeByArtworkIdAsync(Guid artworkId);
        Task<List<ArtworkType>> GetArtworkTypeAsyncByTypeOfArtworkId(Guid typeOfArtworkId);        
    }
}
