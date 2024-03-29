using Artworks_Sharing_Plaform_Api.Model;
using Artworks_Sharing_Plaform_Api.Model.Dto.ReqDto;
using Artworks_Sharing_Plaform_Api.Model.Dto.ResDto;

namespace Artworks_Sharing_Plaform_Api.Service.Interface
{
    public interface ITypeOfArtworkService
    {
        Task<bool> CreateTypeOfArtworkAsync(IFormFile? file, TypeOfArtworkReqDto dto);
        Task<List<GetTypeOfArtworkResDto>> GetListTypeOfArtworkAsync();
        Task<List<GetTypeOfArtworkResDto>> GetListTypeOfArtworkAsyncByRoleAdmin();
        Task<bool> UpdateTypeOfArtworkAsync(UpdateTypeOfArtworkReqDto dto);
        Task<bool> DeleteTypeOfArtworkAsync(Guid id);
        Task<bool> ActiveTypeOfArtworkAsync(Guid id);
        Task<bool> DeActiveTypeOfArtworkAsync(Guid id);
        Task<bool> ChangeStatusTypeOfArtworkByAdminAsync(ChangeStatusRequestDto dto);
        Task<GetTypeOfArtworkResDto> GetTypeOfArtworkAsyncById(Guid id);
        Task<List<GetTypeOfArtworkResDto>> GetListTypeOfArtworkAsyncByRoleCreator();
        Task<List<GetTypeOfArtworkResDto>> GetListTypeOfArtworkAsyncByTypeOfArtworkNameAndRoleCreator(string type);
        Task<List<GetTypeOfArtworkResDto>> GetListRequestTypeOfArtwork();
        Task<List<GetTypeOfArtworkResDto>> GetListPendingTypeOfArtworkAsync();
    }
}