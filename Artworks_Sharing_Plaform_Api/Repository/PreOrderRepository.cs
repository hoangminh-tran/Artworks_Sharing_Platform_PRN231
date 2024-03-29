using Artworks_Sharing_Plaform_Api.AppDatabaseContext;
using Artworks_Sharing_Plaform_Api.Model;
using Artworks_Sharing_Plaform_Api.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Artworks_Sharing_Plaform_Api.Repository
{
    public class PreOrderRepository : IPreOrderRepository
    {
        private readonly ArtworksSharingPlaformDatabaseContext _context;
        public PreOrderRepository(ArtworksSharingPlaformDatabaseContext context)
        {
            _context = context;
        }

        public async Task<bool> CreatePreOrderAsync(PreOrder preOrder)
        {
            try
            {
                await _context.PreOrders.AddAsync(preOrder);
                await _context.SaveChangesAsync();
                return true;
            } catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> DeletePreOrderAsync(PreOrder preOrder)
        {
            try
            {
                _context.PreOrders.Remove(preOrder);
                await _context.SaveChangesAsync();
                return true;
            } catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<PreOrder>> GetListPreOrderByCustomerIdAsync(Guid customerId)
        {
            try
            {
                return await _context.PreOrders.Where(p => p.CustomerId.Equals(customerId)).ToListAsync();
            } catch (Exception)
            {
                throw;
            }
        }

        public async Task<PreOrder?> GetPreOrderByArtworkIdAndCustomerIdAsync(Guid artworkId, Guid customerId)
        {
            try
            {
                return await _context.PreOrders.FirstOrDefaultAsync(p => p.ArtworkId.Equals(artworkId) && p.CustomerId.Equals(customerId));
            } catch (Exception)
            {
                throw;
            }
        }

        public async Task<PreOrder?> GetPreOrderByIdAsync(Guid preOrderId)
        {
            try
            {
                return await _context.PreOrders.FirstOrDefaultAsync(p => p.Id.Equals(preOrderId));
            } catch (Exception)
            {
                throw;
            }
        }
    }
}
