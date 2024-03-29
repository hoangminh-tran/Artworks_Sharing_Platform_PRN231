using Artworks_Sharing_Plaform_Api.Model.Abstract;
using System.ComponentModel.DataAnnotations.Schema;

namespace Artworks_Sharing_Plaform_Api.Model
{
    [Table("Order", Schema = "dbo")]
    public class Order : Common
    {
        [Column("AccountId")]
        public Guid AccountId { get; set; }
        public Account Account { get; set; } = null!;        

        [Column("Payment")]
        public string? Payment { get; set; }

        [Column("StatusId")]
        public Guid StatusId { get; set; }
        public Status Status { get; set; } = null!;

        public ICollection<Artwork>? Artworks { get; set; }
    }
}
