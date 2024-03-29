using Artworks_Sharing_Plaform_Api.Model.Abstract;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Artworks_Sharing_Plaform_Api.Model
{
    [Table("TypeOfArtwork", Schema = "dbo")]
    public class TypeOfArtwork : Common
    {
        [Column("AccountId")]
        public Guid? AccountId { get; set; }
        public Account? Account { get; set; }

        [Required]
        [Column("Type")]        
        public string Type { get; set; } = null!;

        [Required]
        [Column("TypeDescription")]
        public string TypeDescription { get; set; } = null!;

        [Required]
        [Column("StatusId")]
        public Guid StatusId { get; set; }
        public Status? Status { get; set; }

        [Column("ArtworkTypeImageDeafault")]
        public byte[]? TypeImageDeafault { get; set; }   
        
        public ICollection<ArtworkType>? ArtworkTypes { get; set; }
        public ICollection<BookingArtworkType>? BookingArtworkTypes { get; set; }
    }
}
