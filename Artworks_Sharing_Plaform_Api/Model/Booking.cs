using Artworks_Sharing_Plaform_Api.Model.Abstract;
using System.ComponentModel.DataAnnotations.Schema;

namespace Artworks_Sharing_Plaform_Api.Model
{
    [Table("Booking", Schema = "dbo")]
    public class Booking : Common
    {
        [Column("UserId")]
        public Guid UserId { get; set; }
        public Account User { get; set; } = null!;

        [Column("CreatorId")]
        public Guid CreatorId { get; set; }
        public Account Creator { get; set; } = null!;

        [Column("StatusId")]
        public Guid StatusId { get; set; }
        public Status Status { get; set; } = null!;

        [Column("Description")]
        public string Description { get; set; } = null!;

        [Column("Price")]
        public decimal Price { get; set; }

        public ICollection<BookingArtworkType>? BookingArtworkTypes { get; set; }
        public ICollection<BookingArtwork>? BookingArtworks { get; set; }
        public ICollection<RequestArtwork>? RequestArtworks { get; set; }
    }
}
