using Artworks_Sharing_Plaform_Api.Model;
using Artworks_Sharing_Plaform_Api.Model.Dto.ReqDto;
using Artworks_Sharing_Plaform_Api.Model.Dto.ResDto;

namespace Artworks_Sharing_Plaform_Api.Service.Interface
{
    public interface IArtworkService
    {
        Task<bool> UploadArtworkByCreatorAsync(IFormFile file, UploadArtworkReqDto imgDes);
        Task<List<GetArtworkByCreatorResDto>> GetListArtworkByCreatorAsync();
        Task<List<GetArtworkByCustomerResDto>> GetListArtworkByCustomerAsync();
        Task<GetArtworkByCreatorResDto> GetArtworkByArtworkIdByCreatorAsync(Guid artworkId);
        Task<GetArtworkByCustomerResDto> GetArtworkByArtworkIdByCustomerAsync(Guid artworkId);
        Task<GetArtworkByGuest> GetArtworkByArtworkIdByGuestAsync(Guid artworkId);
        Task<List<GetArtworkResDto>> GetListArtworkOwnByUserAsync();
        Task<bool> UpdateArtwork(UpdateArtworkDto artworkRequest);
        Task<List<GetPublicArtworkResDto>> GetListArtworkAsync();
        Task<List<GetArtworkResDto>> GetListArtworkByTypeOfArtworkIdAsync(Guid typeOfArtworkID);        
        Task<List<GetArtworkResDto>> GetListArtworkByArtworkNameAsync(string? artworkName);
        Task<List<GetArtworkResDto>> GetListArtworkByCreatorIdAsync(Guid CreatorId);
        Task<List<GetArtworkResDto>> GetListArtworkByFilterListTypeOfArtworkAndArtistAsync(FilterArtworkByListTypeAndArtistReqDto? dto);
    }
}
