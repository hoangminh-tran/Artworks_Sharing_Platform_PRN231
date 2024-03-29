using Artworks_Sharing_Plaform_Api.Model.Abstract;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Artworks_Sharing_Plaform_Api.Model
{
    [Table("Status", Schema = "dbo")]
    public class Status : Common
    {
        [Required]
        [StringLength(200), MinLength(1)]
        [Column("StatusName")]        
        public string StatusName { get; set; } = null!;    
        
        public ICollection<Account>? Accounts { get; set; }
        public ICollection<Artwork>? Artworks { get; set; }
        public ICollection<Booking>? Bookings { get; set; }
        public ICollection<Complant>? Complants { get; set; }
        public ICollection<Order>? Orders { get; set; }
        public ICollection<RequestArtwork>? RequestArtworks { get; set;}
        public ICollection<PreOrder>? PreOrders { get; set; }
        public ICollection<PaymentHistory>? PaymentHistories { get; set; }
    }
}
