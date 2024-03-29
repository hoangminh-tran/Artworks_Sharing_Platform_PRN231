using Artworks_Sharing_Plaform_Api.Model.Abstract;
using System.ComponentModel.DataAnnotations.Schema;

namespace Artworks_Sharing_Plaform_Api.Model
{
    [Table("LikeBy", Schema = "dbo")]
    public class LikeBy : Common
    {
        [Column("ArtworkId")]
        public Guid? ArtworkId { get; set; }
        public Artwork? Artwork { get; set; }

        [Column("CommentId")]
        public Guid? CommentId { get; set; }
        public Comment? Comment { get; set; }

        [Column("PostId")]
        public Guid? PostId { get; set; }
        public Post? Post { get; set; }

        [Column("AccountId")]
        public Guid AccountId { get; set; }
        public Account Account { get; set; } = null!;
    }
}
