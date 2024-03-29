using Artworks_Sharing_Plaform_Api.AppDatabaseContext;
using Artworks_Sharing_Plaform_Api.Model;
using Artworks_Sharing_Plaform_Api.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Artworks_Sharing_Plaform_Api.Repository
{
    public class BookingArtworkTypeRepository : IBookingArtworkTypeRepository
    {
        private readonly ArtworksSharingPlaformDatabaseContext _context;

        public BookingArtworkTypeRepository(ArtworksSharingPlaformDatabaseContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateBookingArtworkTypeAsync(BookingArtworkType bookingArtworkType)
        {
            try
            {
                await _context.BookingArtworkTypes.AddAsync(bookingArtworkType);
                await _context.SaveChangesAsync();
                return true;
            } catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<BookingArtworkType>> GetListBookingArtworkTypeByBookingIdAsync(Guid bookingId)
        {
            try
            {
                return await _context.BookingArtworkTypes.Where(b => b.BookingId.Equals(bookingId)).ToListAsync();
            } catch (Exception)
            {
                throw;
            }
        }
    }
}
