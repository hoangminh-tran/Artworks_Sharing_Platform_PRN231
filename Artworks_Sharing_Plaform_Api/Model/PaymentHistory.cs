using Artworks_Sharing_Plaform_Api.Model.Abstract;
using System.ComponentModel.DataAnnotations.Schema;

namespace Artworks_Sharing_Plaform_Api.Model
{
    public class PaymentHistory : Common
    {
        [Column("AccountId")]
        public Guid AccountId { get; set; }
        public Account Account { get; set; } = null!;

        [Column("Amount")]
        public decimal Amount { get; set; }

        [Column("Code")]
        public string Code { get; set; } = null!;

        [Column("StatusId")]
        public Guid StatusId { get; set; } // CONFIRMED, PENDING, CANCELLED
        public Status? Status { get; set; } 
    }
}
