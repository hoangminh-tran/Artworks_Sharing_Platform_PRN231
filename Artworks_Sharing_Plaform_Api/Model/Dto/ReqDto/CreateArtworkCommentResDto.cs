namespace Artworks_Sharing_Plaform_Api.Model.Dto.ReqDto
{
    public class CreateArtworkCommentResDto
    {
        public Guid ArtworkId { get; set; }
        public string Comment { get; set; } = null!;
    }
}
