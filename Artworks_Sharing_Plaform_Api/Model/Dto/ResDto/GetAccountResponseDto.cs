using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Artworks_Sharing_Plaform_Api.Model.Dto.ResDto
{
    public class GetAccountResponseDto
    {
        public Guid id { get; set; }
        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string? PhoneNumber { get; set; } = null;

        public string StatusName { get; set; } = null!;

        public string RoleName { get; set; } = null!;
    }
}
