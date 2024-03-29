using Artworks_Sharing_Plaform_Api.Model.Abstract;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Artworks_Sharing_Plaform_Api.Model
{
    [Table("Account", Schema = "dbo")]
    public class Account : Common
    {
        [Required]
        [StringLength(50), MinLength(1)]
        [Column("FirstName")]        
        public string FirstName { get; set; } = null!;

        [Required]
        [StringLength(50), MinLength(1)]
        [Column("LastName")]        
        public string LastName { get; set; } = null!;
        
        [Required]
        [EmailAddress]
        [Column("Email")]        
        public string Email { get; set; } = null!;

        [Column("PasswordHash")]
        public string PasswordHash { get; set; } = null!;

        [Column("PasswordSalt")]
        public string PasswordSalt { get; set; } = null!;

        [Column("PhoneNumber")]        
        public string? PhoneNumber { get; set; } = null;

        [Column("StatusId")]
        public Guid StatusId { get; set; }
        public Status Status { get; set; } = null!;

        [Column("RoleId")]
        public Guid RoleId { get; set; }
        public Role Role { get; set; } = null!;

        [Column("Balance")]
        public decimal Balance { get; set; } = 0;

        [Column("OtpCode")]
        public string? OtpCode { get; set; } = null;

        [Column("OtpCodeCreated")]
        public DateTime? OtpCodeCreated { get; set; } = null;

        [Column("OtpCodeExpired")]
        public DateTime? OtpCodeExpired { get; set; } = null;

        public ICollection<Artwork>? CreatorArtwork { get; set; }
        public ICollection<Booking>? UserBooking { get; set; }
        public ICollection<Booking>? CreatorBooking { get; set; }
        public ICollection<Comment>? AccountComment { get; set; }
        public ICollection<Complant>? AccountComplant { get; set; }
        public ICollection<Complant>? ManageIssuseAccount { get; set; }
        public ICollection<LikeBy>? AccountLikeBy { get; set; }
        public ICollection<Order>? AccountOrder { get; set; }
        public ICollection<Post>? CreatorPost { get; set; }
        public ICollection<Sharing>? UserSharing { get; set; }
        public ICollection<UserFollow>? UserFollow { get; set; }
        public ICollection<UserFollow>? UserFollowing { get; set; }
        public ICollection<PreOrder>? PreOrders { get; set; }
        public ICollection<PaymentHistory>? PaymentHistories { get; set; }
    }
}
