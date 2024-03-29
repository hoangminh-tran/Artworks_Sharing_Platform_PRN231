using Artworks_Sharing_Plaform_Api.Model;
using System.Threading.Tasks.Sources;

namespace Artworks_Sharing_Plaform_Api.Repository.Interface
{
    public interface IBookingRepository
    {
        Task<Booking> CreateBookingAsync(Booking booking);
        Task<Booking?> GetBookingByIdAsync(Guid bookingId);
        Task<bool> UpdateBookingAsync(Booking booking);
        Task<List<Booking>> GetListBookingByCustomerIdAsync(Guid customerId);
        Task<List<Booking>> GetListBookingByCreatorIdAsync(Guid creatorId);
        Task<List<Booking>> GetListBookingByAdminIdAsync();
    }
}
