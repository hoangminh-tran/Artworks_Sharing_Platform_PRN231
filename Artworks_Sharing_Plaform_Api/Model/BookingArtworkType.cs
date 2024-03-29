using Artworks_Sharing_Plaform_Api.Model.Abstract;
using System.ComponentModel.DataAnnotations.Schema;

namespace Artworks_Sharing_Plaform_Api.Model
{
    [Table("BookingArtworkType", Schema = "dbo")]
    public class BookingArtworkType : Common
    {
        [Column("BookingId")]
        public Guid BookingId { get; set; }
        public Booking Booking { get; set; } = null!;

        [Column("TypeOfArtworkId")]
        public Guid TypeOfArtworkId { get; set; }
        public TypeOfArtwork TypeOfArtwork { get; set; } = null!;
    }
}
