using System.ComponentModel.DataAnnotations;

namespace Artworks_Sharing_Plaform_Api.Model.Dto.ReqDto
{
    public class ChangeStatusBookingByCreatorReqDto
    {
        [Required]
        public Guid BookingId { get; set; }

        [Required]
        public bool IsAccept { get; set; }
    }
}
