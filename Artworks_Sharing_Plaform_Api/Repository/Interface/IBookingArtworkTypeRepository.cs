using Artworks_Sharing_Plaform_Api.Model;

namespace Artworks_Sharing_Plaform_Api.Repository.Interface
{
    public interface IBookingArtworkTypeRepository
    {
        Task<bool> CreateBookingArtworkTypeAsync(BookingArtworkType bookingArtworkType);
        Task<List<BookingArtworkType>> GetListBookingArtworkTypeByBookingIdAsync(Guid bookingId);
    }
}
