using Artworks_Sharing_Plaform_Api.AppDatabaseContext;
using Artworks_Sharing_Plaform_Api.Model;
using Artworks_Sharing_Plaform_Api.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Artworks_Sharing_Plaform_Api.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ArtworksSharingPlaformDatabaseContext _db;
        public OrderRepository(ArtworksSharingPlaformDatabaseContext db)
        {
            _db = db;
        }

        public async Task<Order> CreateOrderAsync(Order order)
        {
            try
            {
                await _db.Orders.AddAsync(order);
                await _db.SaveChangesAsync();
                return order;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Order>> GetListOrder()
        {
            try
            {
                return await _db.Orders
                    .Include(_ => _.Artworks)
                    .Include(_ => _.Account)
                    .Include(_ => _.Status)
                    .ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Order>> GetListOrderByCustomerIdAsync(Guid customerId)
        {
            try
            {
                return await _db.Orders
                    .Include(_ => _.Artworks)
                    .Include(_ => _.Account)
                    .Include(_ => _.Status)
                    .Where(x => x.AccountId == customerId).ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Order?> GetOrderByIdAsync(Guid id)
        {
            try
            {
                return await _db.Orders
                    .Include(_ => _.Artworks)
                    .Include(_ => _.Account)
                    .Include(_ => _.Status)
                    .Where(x => x.Id == id).FirstOrDefaultAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public IQueryable<Order> GetAll()
        {
            return _db.Set<Order>()
                      .Include(_ => _.Artworks)
                      .Include(_ => _.Account)
                      .Include(_ => _.Status);
        }
    }
}
