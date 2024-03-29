using Artworks_Sharing_Plaform_Api.Model.Abstract;
using System.ComponentModel.DataAnnotations.Schema;

namespace Artworks_Sharing_Plaform_Api.Model
{
    [Table("Sharing", Schema = "dbo")]
    public class Sharing : Common
    {
        [Column("PostId")]
        public Guid PostId { get; set; }
        public Post Post { get; set; } = null!;

        [Column("AccountId")]
        public Guid AccountId { get; set; }
        public Account Account { get; set; } = null!;

        [Column("Description")]
        public string Description { get; set; } = null!;

        public ICollection<Complant>? Complants { get; set; }
    }
}
