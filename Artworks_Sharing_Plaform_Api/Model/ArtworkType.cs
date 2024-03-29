using Artworks_Sharing_Plaform_Api.Model.Abstract;
using System.ComponentModel.DataAnnotations.Schema;

namespace Artworks_Sharing_Plaform_Api.Model
{
    [Table("ArtworkType", Schema = "dbo")]
    public class ArtworkType : Common
    {
        [Column("ArtworkId")]
        public Guid ArtworkId { get; set; }
        public Artwork Artwork { get; set; } = null!;

        [Column("TypeOfArtworkId")]
        public Guid TypeOfArtworkId { get; set; }
        public TypeOfArtwork TypeOfArtwork { get; set; } = null!;        
    }
}
