using System.ComponentModel.DataAnnotations;

namespace Artworks_Sharing_Plaform_Api.Model.Dto.ReqDto;

public class ChangePassowordAuthenDTO
{
    [Required]
    public string? OldPasssword { get; set; }
    [Required]
    public string NewPassword { get; set; }

}
