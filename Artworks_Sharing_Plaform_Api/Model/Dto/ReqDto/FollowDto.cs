using System.ComponentModel.DataAnnotations;

namespace Artworks_Sharing_Plaform_Api.Model.Dto.ReqDto
{
    public class FollowDto
    {
        [Required]
        public Guid CreatorId { get; set; }
    }
}
