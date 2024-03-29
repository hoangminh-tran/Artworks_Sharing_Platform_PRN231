using Artworks_Sharing_Plaform_Api.Model.Dto.ResDto;

namespace Artworks_Sharing_Plaform_Api.Service.Interface
{
    public interface IOrderService
    {
        Task<bool> BuyArtworkAsync(Guid artworkId);
        Task<List<GetOrderDto>> GetListOrderByCustomerIdAsync(Guid accountId);
        Task<List<GetOrderDto>> GetAllOrder();
        Task<GetOrderDto> GetOrderById(Guid orderId);
    }
}
