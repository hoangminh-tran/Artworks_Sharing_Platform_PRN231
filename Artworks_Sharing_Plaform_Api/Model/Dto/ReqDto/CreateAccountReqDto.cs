using System.ComponentModel.DataAnnotations;

namespace Artworks_Sharing_Plaform_Api.Model.Dto.ReqDto
{
    public class CreateAccountReqDto
    {
        [Required]
        [StringLength(50, MinimumLength = 1)]
        public string FirstName { get; set; } = null!;

        [Required]
        [StringLength(50, MinimumLength = 1)]
        public string LastName { get; set; } = null!;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [StringLength(11, MinimumLength = 10)]
        public string? PhoneNumber { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 8)]
        public string Password { get; set; } = null!;
    }
}
