using Artworks_Sharing_Plaform_Api.Model;
using Artworks_Sharing_Plaform_Api.Model.Dto.ResDto;

namespace Artworks_Sharing_Plaform_Api.Service.Interface
{
    public interface IPreOrderService
    {
        Task<bool> CreatePreOrderAsync(Guid artworkId);
        Task<List<GetPreOrderByCustomerResDto>> GetListPreOrderByCustomerIdAsync();
        Task<bool> DeletePreOrderAsync(Guid preOrderId);
    }
}
