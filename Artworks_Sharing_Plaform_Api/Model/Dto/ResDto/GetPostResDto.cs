namespace Artworks_Sharing_Plaform_Api.Model.Dto.ResDto
{
    public class GetPostResDto
    {
        public Guid PostId { get; set; }
        public string ContentPost { get; set; } = null!;
        public string CreatorName { get; set; } = null!;
        public DateTime CreateDateTime { get; set; }
        public int LikeCount { get; set; }
        public List<GetArtworkResDto> ListArtwork { get; set; } = null!;
        public bool IsLike {  get; set; } 
        public Guid CreatorId { get; set; }
    }
}
