namespace Artworks_Sharing_Plaform_Api.Model.Dto.ResDto
{
    public class GetBookingForCustomerResDto
    {
        public Guid BookingId { get; set; }
        public string? CreatorName { get; set; }
        public List<GetTypeOfArtworkResDto> ListTypeOfArtwork { get; set; } = null!;
        public string StatusName { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
        public byte[]? Image { get; set; }
        public List<GetRequestBookingResDto>? RequestBooking { get; set; }
        public DateTime CreateDateTime { get; set; }
    }
}
