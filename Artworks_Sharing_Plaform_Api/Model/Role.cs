using Artworks_Sharing_Plaform_Api.Model.Abstract;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Artworks_Sharing_Plaform_Api.Model
{
    [Table("Role", Schema = "dbo")]
    public class Role : Common
    {
        [Required]
        [StringLength(50), MinLength(1)]
        [Column("RoleName")]        
        public string RoleName { get; set; } = null!;    
        
        public ICollection<Account>? Accounts { get; set; } = null!;
    }
}
