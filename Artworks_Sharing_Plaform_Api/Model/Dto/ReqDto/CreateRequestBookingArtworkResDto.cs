namespace Artworks_Sharing_Plaform_Api.Model.Dto.ReqDto
{
    public class CreateRequestBookingArtworkResDto
    {
        public Guid BookingId { get; set; }
        public string ContentRequest { get; set; } = null!;
    }
}
