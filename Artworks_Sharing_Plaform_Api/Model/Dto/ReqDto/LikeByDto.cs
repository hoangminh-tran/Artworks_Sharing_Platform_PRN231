using System.ComponentModel.DataAnnotations.Schema;

namespace Artworks_Sharing_Plaform_Api.Model.Dto.ReqDto
{
    public class LikeByDto
    {
        [Column("ArtworkId")]
        public Guid ArtworkId { get; set; }
    }
}
