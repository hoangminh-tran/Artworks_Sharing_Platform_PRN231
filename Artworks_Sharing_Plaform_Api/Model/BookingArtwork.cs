using Artworks_Sharing_Plaform_Api.Model.Abstract;
using System.ComponentModel.DataAnnotations.Schema;

namespace Artworks_Sharing_Plaform_Api.Model
{
    [Table("BookingArtwork", Schema = "dbo")]
    public class BookingArtwork : Common
    {
        [Column("BookingId")]
        public Guid? BookingId { get; set; }
        public Booking Booking { get; set; } = null!;

        [Column("ArtworkId")]
        public Guid ArtworkId { get; set; }
        public Artwork Artwork { get; set; } = null!;

        [Column("RequestArtworkId")]
        public Guid? RequestArtworkId { get; set; }
        public RequestArtwork RequestArtwork { get; set; } = null!;
    }
}
