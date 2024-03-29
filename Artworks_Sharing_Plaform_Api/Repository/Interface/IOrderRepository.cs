using Artworks_Sharing_Plaform_Api.Model;

namespace Artworks_Sharing_Plaform_Api.Repository.Interface
{
    public interface IOrderRepository
    {
        Task<Order?> GetOrderByIdAsync(Guid id);
        Task<List<Order>> GetListOrderByCustomerIdAsync(Guid customerId);
        Task<Order> CreateOrderAsync(Order order);
        Task<List<Order>> GetListOrder();
        IQueryable<Order> GetAll();
    }
}
