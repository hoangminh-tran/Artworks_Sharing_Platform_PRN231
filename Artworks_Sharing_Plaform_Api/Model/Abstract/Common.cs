using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Artworks_Sharing_Plaform_Api.Model.Abstract
{
    public class Common
    {
        [Key]
        [Column("Id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Column("CreateDateTime")]
        public DateTime CreateDateTime { get; set; } = DateTime.Now;

        [Column("UpdateDateTime")]
        public DateTime? UpdateDateTime { get; set; } = null;

        [Column("DeleteDateTime")]
        public DateTime? DeleteDateTime { get; set; } = null;
    }
}
