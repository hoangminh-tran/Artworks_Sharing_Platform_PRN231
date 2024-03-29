using Artworks_Sharing_Plaform_Api.Model.Abstract;
using System.ComponentModel.DataAnnotations.Schema;

namespace Artworks_Sharing_Plaform_Api.Model
{
    [Table("UserFollow", Schema = "dbo")]
    public class UserFollow : Common
    {
        [Column("UserId")]
        public Guid UserId { get; set; }
        public Account User { get; set; } = null!;

        [Column("FollowingId")]
        public Guid FollowingId { get; set; }
        public Account Following { get; set; } = null!;
    }
}
