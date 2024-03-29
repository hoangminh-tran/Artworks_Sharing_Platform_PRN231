using Artworks_Sharing_Plaform_Api.Model;
using Artworks_Sharing_Plaform_Api.Model.Dto.ReqDto;
using Artworks_Sharing_Plaform_Api.Model.Dto.ResDto;

namespace Artworks_Sharing_Plaform_Api.Service.Interface
{
    public interface IComplantService
    {
        Task<bool> UpdateComplaintAsync(ComplaintRequestDto request);
        Task<bool> CreatePostComplaint(ComplaintPostRequestDto complaintRequest);
        Task<bool> CreateCommentComplaint(ComplaintCommentRequestDto complaintRequest);
        Task<bool> CreateArtworkComplaint(ComplaintArtworkRequestDto complaintRequest);

    }
}
