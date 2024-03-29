namespace Artworks_Sharing_Plaform_Api.Model.Dto.ReqDto
{
    public class ChangeStatusRequestByCreatorResDto
    {
        public Guid RequestBookingId { get; set; }
        public bool IsAccept { get; set; }
    }
}
