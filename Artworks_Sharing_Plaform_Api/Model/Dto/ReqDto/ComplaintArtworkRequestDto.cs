using System.ComponentModel.DataAnnotations;

namespace Artworks_Sharing_Plaform_Api.Model.Dto.ReqDto
{
    public class ComplaintPostRequestDto
    {
        [Required] 
        public string ComplainType { get; set; } = null!;
        [Required]
        public Guid PostId { get; set; }
        [Required]
        public string ComplaintDescription { get; set; } = null!;
    }

    public class ComplaintArtworkRequestDto
    {
        [Required]
        public string ComplainType { get; set; } = null!;
        [Required]
        public Guid ArtworkId { get; set; }
        [Required]
        public string ComplaintDescription { get; set; } = null!;
    }
}
