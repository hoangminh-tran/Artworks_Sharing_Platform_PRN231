using Artworks_Sharing_Plaform_Api.Model;
using Artworks_Sharing_Plaform_Api.Model.Dto.ReqDto;
using Artworks_Sharing_Plaform_Api.Model.Dto.ResDto;

namespace Artworks_Sharing_Plaform_Api.Service.Interface
{
    public interface IArtworkTypeService
    {
        Task<bool> CreateArtworkTypeByListTypeOfArtworkAsync(CreateAndUpdateListArtworkTypeReqDto reqDto);
        Task<bool> UpdateArtworkTypeByListTypeOfArtworkAsync(CreateAndUpdateListArtworkTypeReqDto reqDto);
        Task<List<GetArtworkTypeResponseDto>> GetArtworkTypeAsyncByTypeOfArtworkId(Guid typeOfArtworkId);
    }
}
