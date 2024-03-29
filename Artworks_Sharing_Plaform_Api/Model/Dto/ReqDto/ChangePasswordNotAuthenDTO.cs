using System.ComponentModel.DataAnnotations;

namespace Artworks_Sharing_Plaform_Api.Model.Dto.ReqDto;

public class ChangePasswordNotAuthenDTO
{
    [Required]
    public string Email { get; set; }
    [Required]
    public string OtpCode { get; set; }
    [Required]
    public string NewPassword { get; set; }

}
