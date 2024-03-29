using System.ComponentModel.DataAnnotations.Schema;

namespace Artworks_Sharing_Plaform_Api.Model.Dto.ReqDto
{
    public class LikeCommentDto
    {
        [Column("CommentId")]
        public Guid CommentId { get; set; }
    }
}
