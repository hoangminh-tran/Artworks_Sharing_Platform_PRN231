using Artworks_Sharing_Plaform_Api.AppDatabaseContext;
using Artworks_Sharing_Plaform_Api.Model;
using Artworks_Sharing_Plaform_Api.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Artworks_Sharing_Plaform_Api.Repository
{
    public class BookingArtworkRepository : IBookingArtworkRepository
    {
        private readonly ArtworksSharingPlaformDatabaseContext _context;

        public BookingArtworkRepository(ArtworksSharingPlaformDatabaseContext context)
        {
            _context = context;
        }

        public async Task<BookingArtwork> CreateBookingArworkAsync(BookingArtwork bookingArtwork)
        {
            try
            {
                await _context.BookingArtworks.AddAsync(bookingArtwork);
                await _context.SaveChangesAsync();
                return bookingArtwork;
            } catch (Exception)
            {
                throw;
            }
        }

        public async Task<BookingArtwork?> GetBookingArtworkByBookingIdAsync(Guid bookingId)
        {
            try
            {
                return await _context.BookingArtworks.FirstOrDefaultAsync(x => x.BookingId == bookingId);
            } catch (Exception)
            {
                throw;
            }
        }

        public async Task<BookingArtwork?> GetBookingArtworkByRequestArtworkIdAsync(Guid bookingId)
        {
            try
            {
                return await _context.BookingArtworks.FirstOrDefaultAsync(x => x.RequestArtworkId == bookingId);
            } catch (Exception)
            {
                throw;
            }
        }
    }
}
