using System.ComponentModel.DataAnnotations;

namespace Artworks_Sharing_Plaform_Api.Model.Dto.ReqDto
{
    public class CreatePostReqDto
    {
        [Required]
        [StringLength(400), MinLength(1)]
        public string ContentPost { get; set; } = null!;

        [Required]
        public List<Guid> ListArtwork { get; set; } = null!;
    }
}
