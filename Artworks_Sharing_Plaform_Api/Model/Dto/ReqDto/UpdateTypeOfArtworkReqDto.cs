using System.ComponentModel.DataAnnotations;

namespace Artworks_Sharing_Plaform_Api.Model.Dto.ReqDto
{
    public class UpdateTypeOfArtworkReqDto
    {
        [Required]
        public Guid typeOfArtworkID { get; set; }

        [Required]
        public string type { get; set; } = null!;

        [Required]
        public string typeDescription { get; set; } = null!;
    }
}
