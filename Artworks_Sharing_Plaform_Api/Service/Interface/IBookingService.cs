using Artworks_Sharing_Plaform_Api.Model.Dto.ReqDto;
using Artworks_Sharing_Plaform_Api.Model.Dto.ResDto;

namespace Artworks_Sharing_Plaform_Api.Service.Interface
{
    public interface IBookingService
    {
        Task<bool> CreateBookingAsync(CreateBookingArtworkReqDto bookingArtworkReqDto);
        Task<bool> UploadArtworkByBookingIdAsync(IFormFile file, CreateUploadArtworkForBookingReqDto createUploadArtworkForBookingReqDto);
        Task<bool> UpdateStatusBookingAsync(UpdateStatusBookingResDto updateStatusBookingResDto);
        Task<bool> CreateRequestBookingArtworkByBookingIdAsync(CreateRequestBookingArtworkResDto createRequestBookingArtworkResDto);
        Task<List<GetBookingForCustomerResDto>> GetListBookingByCustomerIdAsync();
        Task<GetBookingForCustomerResDto> GetBookingByBookingIdByCustomerAsync(Guid bookingId);
        Task<List<GetBookingForCreatorResDto>> GetListBookingByCreatorIdAsync();
        Task<GetBookingForCreatorResDto> GetBookingByBookingIdByCreatorAsync(Guid bookingId);
        Task<bool> UploadArtworkByRequestArtworkAsync(IFormFile file, CreateUploadArtworkForRequestBookingArtworkResDto resDto);      
        Task<bool> ChangeStatusBookingByCreatorAsync(ChangeStatusBookingByCreatorReqDto changeStatusBookingByCreatorReqDto);
        Task<List<GetBookingForAdminResDto>> GetListBookingByAdminAsync();
        Task<bool> ChangeStatusRequestByCreatorAsync(ChangeStatusRequestByCreatorResDto changeStatusRequestByCreatorResDto);
        Task<bool> ChangeStatusRequestBookingByCustomerAsync(ChangeStatusRequestBookingByCustomerReqDto changeStatusRequestBookingByCustomerReqDto);
    }
}
