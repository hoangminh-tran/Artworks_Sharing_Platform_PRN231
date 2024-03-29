namespace Artworks_Sharing_Plaform_Api.Model.Dto.ReqDto
{
    public class CreatePostCommentResDto
    {
        public Guid PostId { get; set; }
        public string Comment { get; set; } = null!;
    }
}
