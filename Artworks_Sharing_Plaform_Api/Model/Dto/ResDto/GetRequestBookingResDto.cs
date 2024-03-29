namespace Artworks_Sharing_Plaform_Api.Model.Dto.ResDto
{
    public class GetRequestBookingResDto
    {
        public Guid RequestBookingId { get; set; }
        public string Description { get; set; } = null!;
        public string StatusName { get; set; } = null!;
        public byte[]? Image { get; set; }       
        public DateTime CreateDateTime { get; set; }
    }
}
