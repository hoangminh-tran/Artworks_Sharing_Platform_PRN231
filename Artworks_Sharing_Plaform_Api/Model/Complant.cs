using Artworks_Sharing_Plaform_Api.Model.Abstract;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Artworks_Sharing_Plaform_Api.Model
{
    [Table("Complant", Schema = "dbo")]
    public class Complant : Common
    {
        [Required]
        [StringLength(300), MinLength(1)]
        [Column("ComplantContent")]        
        public string ComplantContent { get; set; } = null!;

        [Column("AccountComplantId")]
        public Guid AccountComplantId { get; set; }
        public Account AccountComplant { get; set; } = null!;

        [Column("PostId")]
        public Guid? PostId { get; set; }
        public Post? Post { get; set; }

        [Column("ArtworkId")]
        public Guid? ArtworkId { get; set; }
        public Artwork? Artwork { get; set; }

        [Column("CommentId")]
        public Guid? CommentId { get; set; }
        public Comment? Comment { get; set; }

        [Column("SharingId")]
        public Guid? SharingId { get; set; }
        public Sharing? Sharing { get; set; }

        [Column("StatusId")]
        public Guid StatusId { get; set; }
        public Status Status { get; set; } = null!;

        [Required]
        [StringLength(200), MinLength(1)]
        [Column("ComplantType")]
        public string ComplantType { get; set; } = null!;

        [Column("ManageIssuseAccountId")]
        public Guid ManageIssuseAccountId { get; set; }
        public Account ManageIssuseAccount { get; set; } = null!;
    }
}
