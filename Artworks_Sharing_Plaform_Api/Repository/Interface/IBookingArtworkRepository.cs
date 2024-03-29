using Artworks_Sharing_Plaform_Api.Model;

namespace Artworks_Sharing_Plaform_Api.Repository.Interface
{
    public interface IBookingArtworkRepository
    {
        Task<BookingArtwork> CreateBookingArworkAsync(BookingArtwork bookingArtwork);
        Task<BookingArtwork?> GetBookingArtworkByBookingIdAsync(Guid artworkId);
        Task<BookingArtwork?> GetBookingArtworkByRequestArtworkIdAsync(Guid bookingId);
    }
}
