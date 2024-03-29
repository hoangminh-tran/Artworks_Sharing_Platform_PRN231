using System.ComponentModel.DataAnnotations;

namespace Artworks_Sharing_Plaform_Api.Model.Dto.ReqDto
{
    public class UploadArtworkReqDto
    {
        [Required]
        [StringLength(50), MinLength(1)]
        public string ArtworkName { get; set; } = null!;

        [Required]
        [StringLength(500), MinLength(1)]
        public string ArtworkDescription { get; set; } = null!;

        [Required]
        public List<Guid> TypeOfArtwork { get; set; } = null!;

        [Required]
        public bool IsPublic { get; set; }
        
        public decimal? ArtworkPrice { get; set; }
    }
}
