using System.ComponentModel.DataAnnotations;

namespace Artworks_Sharing_Plaform_Api.Model.Dto.ReqDto
{
    public class ChangeStatusRequestDto
    {
        [Required]
        public Guid Id {  get; set; }
        [Required]
        public string StatusName { get; set; }
    }
}
