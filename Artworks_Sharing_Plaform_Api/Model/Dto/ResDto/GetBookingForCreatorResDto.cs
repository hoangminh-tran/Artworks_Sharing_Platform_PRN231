namespace Artworks_Sharing_Plaform_Api.Model.Dto.ResDto
{
    public class GetBookingForCreatorResDto
    {
        public Guid BookingId { get; set; }
        public string? UserName { get; set; }
        public List<GetTypeOfArtworkResDto> ListTypeOfArtwork { get; set; } = null!;
        public string StatusName { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
        public byte[]? Image { get; set; }
        public List<GetRequestBookingResDto>? RequestBooking { get; set; }
        public DateTime CreateDateTime { get; set; }
    }
}
