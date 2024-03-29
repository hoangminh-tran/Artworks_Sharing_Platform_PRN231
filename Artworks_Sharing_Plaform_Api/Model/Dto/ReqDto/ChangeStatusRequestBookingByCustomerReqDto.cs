namespace Artworks_Sharing_Plaform_Api.Model.Dto.ReqDto
{
    public class ChangeStatusRequestBookingByCustomerReqDto
    {
        public Guid? BookingId { get; set; }
        public Guid? RequestBookingId { get; set; }
        public bool IsAccept { get; set; }
    }
}
