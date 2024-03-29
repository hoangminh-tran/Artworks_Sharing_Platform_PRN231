using System.ComponentModel.DataAnnotations;

namespace Artworks_Sharing_Plaform_Api.Model.Dto.ReqDto
{
    public class ComplaintCommentRequestDto
    {
        [Required]
        public string ComplainType { get; set; } = null!;

        [Required]
        public Guid CommentId { get; set; }

        [Required]
        public string ComplaintDescription { get; set; } = null!;
    }
}
