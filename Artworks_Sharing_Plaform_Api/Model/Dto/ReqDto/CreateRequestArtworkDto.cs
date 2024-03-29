using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Artworks_Sharing_Plaform_Api.Model.Dto.ReqDto
{
    public class CreateRequestArtworkDto
    {
        [Required]
        public Guid CreatorRequestId { get; set; }

        public string? Description { get; set; }

        public double? Price { get; set; }
    }
}
