using System.ComponentModel.DataAnnotations;

namespace Artworks_Sharing_Plaform_Api.Model.Dto.ReqDto
{
    public class SharePostArtworkDto
    {
        public string? DescriptionOfSharing { get; set; }
        [Required]
        public Guid PostId { get; set; }
    }
}
