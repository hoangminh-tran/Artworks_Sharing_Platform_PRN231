using Artworks_Sharing_Plaform_Api.Model.Abstract;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Artworks_Sharing_Plaform_Api.Model
{
    [Table("RequestArtwork", Schema = "dbo")]
    public class RequestArtwork : Common
    {
        [Column("BookingId")]
        public Guid BookingId { get; set; }
        public Booking Booking { get; set; } = null!;       
                
        [StringLength(300), MinLength(1)]
        [Column("Description")]
        public string? Description { get; set; }        

        [Column("StatusId")]
        public Guid StatusId { get; set; }
        public Status Status { get; set; } = null!;       

        public ICollection<BookingArtwork>? BookingArtworks { get; set; } = null!;
    }
}
