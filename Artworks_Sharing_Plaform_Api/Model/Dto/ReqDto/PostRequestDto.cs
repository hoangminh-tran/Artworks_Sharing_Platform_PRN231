using System.ComponentModel.DataAnnotations.Schema;

namespace Artworks_Sharing_Plaform_Api.Model.Dto.ReqDto
{
    public class PostRequestDto
    {
        public Guid PostId { get; set; }
        public string ContentPost { get; set; } = null!;
        public Guid CreatorId { get; set; }
    }
}
