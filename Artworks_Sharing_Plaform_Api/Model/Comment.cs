using Artworks_Sharing_Plaform_Api.Model.Abstract;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Artworks_Sharing_Plaform_Api.Model
{
    [Table("Comment", Schema = "dbo")]
    public class Comment : Common
    {
        [Required]
        [StringLength(300), MinLength(1)]
        [Column("Content")]
        public string Content { get; set; } = null!;

        [Column("AccountId")]
        public Guid AccountId { get; set; }
        public Account Account { get; set; } = null!;

        [Column("ArtworkId")]
        public Guid? ArtworkId { get; set; }
        public Artwork? Artwork { get; set; }

        [Column("PostId")]
        public Guid? PostId { get; set; }
        public Post? Post { get; set; }

        public ICollection<Complant>? Complants { get; set; }
        public ICollection<LikeBy>? LikeBys { get; set; }

    }
}
