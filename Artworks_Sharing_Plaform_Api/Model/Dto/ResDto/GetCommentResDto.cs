namespace Artworks_Sharing_Plaform_Api.Model.Dto.ResDto
{
    public class GetCommentResDto
    {
        public string CommentId { get; set; }
        public string AccountName { get; set; } = null!;
        public DateTime CreateDateTime { get; set; }
        public string Content { get; set; } = null!;
        public bool IsCommentByAccount { get; set; }
    }
}
