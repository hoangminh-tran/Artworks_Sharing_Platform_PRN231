using System.ComponentModel.DataAnnotations;

namespace Artworks_Sharing_Plaform_Api.Model.Dto.ReqDto
{
    public class CreateBookingArtworkReqDto
    {
        [Required]
        public Guid CreatorId { get; set; }

        [Required]
        public string ContentBooking { get; set; } = null!;

        [Required]
        public List<Guid> ListTypeOfArtwork { get; set; } = null!;

        [Required]
        public decimal Price { get; set; }
    }
}
