using Artworks_Sharing_Plaform_Api.Model;
using Artworks_Sharing_Plaform_Api.Model.Dto.ResDto;

namespace Artworks_Sharing_Plaform_Api.Repository.Interface
{
    public interface ITypeOfArtworkRepository
    {
        Task<bool> CreateTypeOfArtwork(TypeOfArtwork typeOfArtwork);
        Task<TypeOfArtwork?> GetTypeOfArtworkByIdAsync(Guid id);        
        Task<List<TypeOfArtwork>> GetListTypeOfArtworkAsync();        
        Task<bool> UpdateTypeOfArtwork(TypeOfArtwork typeOfArtwork);
        Task<List<TypeOfArtwork>> GetListTypeOfArtworkByTypeOfArtworkNameAsync(string type);
        Task<TypeOfArtwork?> GetTypeOfArtworkByIdAndStatusAsync(Guid id, Guid statusId);
    }
}
