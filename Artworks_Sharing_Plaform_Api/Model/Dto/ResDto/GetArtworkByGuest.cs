namespace Artworks_Sharing_Plaform_Api.Model.Dto.ResDto
{
    public class GetArtworkByGuest
    {
        public string CreatorName { get; set; }
        public string ArtworkName { get; set; } = null!;
        public string Description { get; set; } = null!;
        public List<GetTypeOfArtworkResDto> ArtworkTypeList { get; set; }
        public byte[] Image { get; set; } = null!;
        public decimal Price { get; set; }
        public bool IsSold { get; set; }
        public DateTime CreateDateTime { get; set; }
        public int LikeCount { get; set; }
        public bool IsLike { get; set; }
        public Guid creatorId { get; set; }
    }
}
