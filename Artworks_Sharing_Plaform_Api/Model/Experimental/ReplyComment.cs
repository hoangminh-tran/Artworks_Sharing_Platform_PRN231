using Artworks_Sharing_Plaform_Api.Model.Abstract;
using System.ComponentModel.DataAnnotations.Schema;

namespace Artworks_Sharing_Plaform_Api.Model.Experimental
{
    [Table("ReplyComment", Schema = "dbo")]
    public class ReplyComment : Common
    {
        [Column("CommentId")]
        public Guid? CommentId { get; set; }
        public Comment? Comment { get; set; }

        [Column("ParentReplyId")]
        public Guid? ParentReplyId { get; set; }
        public ReplyComment? ParentReply { get; set; }

        [Column("UserCommentId")]
        public Guid UserCommentId { get; set; }
        public Account UserComment { get; set; } = null!;

        [Column("ReplyText")]
        public string ReplyText { get; set; } = null!;

        [Column("LikeCount")]
        public int LikeCount { get; set; } = 0;
    }
}
