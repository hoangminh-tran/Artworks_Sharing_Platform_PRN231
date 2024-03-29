using Artworks_Sharing_Plaform_Api.AppDatabaseContext;
using Artworks_Sharing_Plaform_Api.Model;
using Artworks_Sharing_Plaform_Api.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Artworks_Sharing_Plaform_Api.Repository
{
    public class BookingRepository : IBookingRepository
    {
        private readonly ArtworksSharingPlaformDatabaseContext _context;
        public BookingRepository(ArtworksSharingPlaformDatabaseContext context)
        {
            _context = context;
        }

        public async Task<Booking> CreateBookingAsync(Booking booking)
        {
            try
            {
                await _context.Booking.AddAsync(booking);
                await _context.SaveChangesAsync();
                return booking;
            } catch (Exception)
            {
                throw;
            }
        }

        public async Task<Booking?> GetBookingByIdAsync(Guid bookingId)
        {
            try
            {
                return await _context.Booking.FirstOrDefaultAsync(b => b.Id.Equals(bookingId));
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Booking>> GetListBookingByAdminIdAsync()
        {
            try
            {
                return await _context.Booking.ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Booking>> GetListBookingByCreatorIdAsync(Guid creatorId)
        {
            try
            {
                return await _context.Booking.Where(b => b.CreatorId.Equals(creatorId)).ToListAsync();
            } catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Booking>> GetListBookingByCustomerIdAsync(Guid customerId)
        {
            try
            {
                return await _context.Booking.Where(b => b.UserId.Equals(customerId)).ToListAsync();
            } catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> UpdateBookingAsync(Booking booking)
        {
            try
            {
                _context.Booking.Update(booking);
                await _context.SaveChangesAsync();
                return true;
            } catch (Exception)
            {
                throw;
            }
        }
    }
}
