using System.ComponentModel.DataAnnotations;

namespace Artworks_Sharing_Plaform_Api.Model.Dto.ReqDto
{
    public class ComplaintRequestDto
    {
        [Required]
        public Guid ComplantID { get; set; }

        [Required]
        public Guid StatusID { get; set; }
    }
}
