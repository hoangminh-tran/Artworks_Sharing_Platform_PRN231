using Artworks_Sharing_Plaform_Api.Model;
using Artworks_Sharing_Plaform_Api.Model.Dto.ReqDto;

namespace Artworks_Sharing_Plaform_Api.Repository.Interface
{
    public interface IComplantRepository
    {
        Task<bool> CreateComplantAsync (Complant complant);
        Task<Complant?> GetComplantByComplantIDAsync(Guid ComplantID);
        Task<bool> UpdateComplaintAsync(Complant complant);                
    }
}
