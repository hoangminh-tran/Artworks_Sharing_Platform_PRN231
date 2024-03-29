using Artworks_Sharing_Plaform_Api.Model.Abstract;
using System.ComponentModel.DataAnnotations.Schema;

namespace Artworks_Sharing_Plaform_Api.Model
{
    [Table("Post", Schema = "dbo")]
    public class Post : Common
    {
        [Column("ContentPost")]
        public string ContentPost { get; set; } = null!;

        [Column("CreatorId")]
        public Guid CreatorId { get; set; }
        public Account Creator { get; set; } = null!;

        public ICollection<Comment>? Comments { get; set; }
        public ICollection<Complant>? Complants { get; set; }
        public ICollection<LikeBy>? LikeBys { get; set; }
        public ICollection<PostArtwork>? PostArtworks { get; set; }
        public ICollection<Sharing>? Sharings { get; set; }

    }
}
