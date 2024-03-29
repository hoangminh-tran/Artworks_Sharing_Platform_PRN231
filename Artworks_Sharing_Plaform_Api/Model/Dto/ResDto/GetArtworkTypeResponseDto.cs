using System.ComponentModel.DataAnnotations.Schema;

namespace Artworks_Sharing_Plaform_Api.Model.Dto.ResDto
{
    public class GetArtworkTypeResponseDto
    {
        public Guid ArtworkId { get; set; }
        public Guid TypeOfArtworkId { get; set; }
    }
}
