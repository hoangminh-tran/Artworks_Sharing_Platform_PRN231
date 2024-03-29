using Artworks_Sharing_Plaform_Api.Model.Abstract;
using System.ComponentModel.DataAnnotations.Schema;

namespace Artworks_Sharing_Plaform_Api.Model
{
    [Table("PostArtwork", Schema = "dbo")]
    public class PostArtwork : Common
    {
        [Column("PostId")]
        public Guid PostId { get; set; }
        public Post Post { get; set; } = null!;

        [Column("ArtworkId")]
        public Guid ArtworkId { get; set; }
        public Artwork Artwork { get; set; } = null!;
    }
}
