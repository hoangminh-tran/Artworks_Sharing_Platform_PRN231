namespace Artworks_Sharing_Plaform_Api.Model.Dto.ResDto
{
    public class GetArtworkResDto
    {
        public Guid ArtworkId { get; set; }
        public string ArtworkName { get; set; } = null!;
        public string ArtworkDescription { get; set; } = null!;
        public string CreatorName { get; set; } = null!;
        public List<GetTypeOfArtworkResDto> TypeOfArtwork { get; set; } = null!;
        public DateTime CreateDateTime { get; set; }
        public int LikeCount { get; set; }
        public byte[] Image { get; set; } = null!;
        public decimal Price { get; set; }
    }
}
