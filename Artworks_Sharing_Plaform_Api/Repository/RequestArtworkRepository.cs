using Artworks_Sharing_Plaform_Api.AppDatabaseContext;
using Artworks_Sharing_Plaform_Api.Model;
using Artworks_Sharing_Plaform_Api.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Artworks_Sharing_Plaform_Api.Repository
{
    public class RequestArtworkRepository : IRequestArtworkRepository
    {
        private readonly ArtworksSharingPlaformDatabaseContext _db;

        public RequestArtworkRepository(ArtworksSharingPlaformDatabaseContext db)
        {
            _db = db;
        }

        public async Task<bool> AcceptOrRejectRequestArtwork(RequestArtwork requestArtwork)
        {
            try
            {
                _db.RequestArtworks.Update(requestArtwork);
                await _db.SaveChangesAsync();
                return true;
            } catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> CreateRequestArtworkAsync(RequestArtwork requestArtwork)
        {
            try
            {
                await _db.RequestArtworks.AddAsync(requestArtwork);
                await _db.SaveChangesAsync();
                return true;
            } catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<RequestArtwork>> GetListRequestArtworkByBookingIdAsync(Guid bookingId)
        {
            try
            {
                return await _db.RequestArtworks.Where(r => r.BookingId.Equals(bookingId)).ToListAsync();
            } catch (Exception)
            {
                throw;
            }
        }

        public async Task<RequestArtwork?> GetRequestArtworkByRequestArtworkId(Guid requestArtworkId)
        {
            try
            {
                return await _db.RequestArtworks.FirstOrDefaultAsync(r => r.Id.Equals(requestArtworkId));                
            } catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> UpdateRequestArtworkAsync(RequestArtwork requestArtwork)
        {
            try
            {
                _db.RequestArtworks.Update(requestArtwork);
                await _db.SaveChangesAsync();
                return true;
            } catch (Exception)
            {
                throw;
            }
        }
    }
}
