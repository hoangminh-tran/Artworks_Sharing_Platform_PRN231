using Artworks_Sharing_Plaform_Api.Model;

namespace Artworks_Sharing_Plaform_Api.Repository.Interface
{
    public interface IPreOrderRepository
    {
        Task<bool> CreatePreOrderAsync(PreOrder preOrder);
        Task<List<PreOrder>> GetListPreOrderByCustomerIdAsync(Guid customerId);
        Task<bool> DeletePreOrderAsync(PreOrder preOrder);
        Task<PreOrder?> GetPreOrderByIdAsync(Guid preOrderId);
        Task<PreOrder?> GetPreOrderByArtworkIdAndCustomerIdAsync(Guid artworkId, Guid customerId);
    }
}
