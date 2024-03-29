using System.ComponentModel.DataAnnotations;

namespace Artworks_Sharing_Plaform_Api.Model.Dto.ReqDto
{
    public class CreateAndUpdateListArtworkTypeReqDto
    {
        [Required]
        public Guid ArtworkId { get; set; }

        [Required]
        public List<Guid> ListTypeOfArtworkId { get; set; } = null!;
    }
}
