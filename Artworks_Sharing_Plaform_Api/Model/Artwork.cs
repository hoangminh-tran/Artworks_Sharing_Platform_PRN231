using Artworks_Sharing_Plaform_Api.Model.Abstract;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Artworks_Sharing_Plaform_Api.Model
{
    [Table("Artwork", Schema = "dbo")]
    public class Artwork : Common
    {
        [Column("CreatorId")]
        public Guid CreatorId { get; set; }
        public Account Creator { get; set; } = null!;

        [Required]
        [StringLength(50), MinLength(1)]
        [Column("Name")]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(500), MinLength(1)]
        [Column("Description")]
        public string Description { get; set; } = null!;

        [Column("Image")]
        public byte[] Image { get; set; } = null!;

        [Column("Price")]
        public decimal? Price { get; set; }

        [Column("StatusId")]
        public Guid StatusId { get; set; }
        public Status Status { get; set; } = null!;

        [Column("OrderId")]
        public Guid? OrderId { get; set; }
        public Order? Order { get; set; }            

        public ICollection<ArtworkType>? ArtworkType { get; set; }
        public ICollection<BookingArtwork>? ArtworkBooking { get; set; }
        public ICollection<Comment>? Comment { get; set; }
        public ICollection<Complant>? Complant { get; set; }
        public ICollection<LikeBy>? LikeBy { get; set; }        
        public ICollection<PostArtwork>? PostArtworks { get; set; }
        public ICollection<PreOrder>? PreOrders { get; set; }
    }
}
