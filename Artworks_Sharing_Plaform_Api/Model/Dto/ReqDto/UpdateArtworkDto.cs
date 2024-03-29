namespace Artworks_Sharing_Plaform_Api.Model.Dto.ReqDto
{
    public class UpdateArtworkDto
    {
        public Guid ArtworkId { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public byte[] Image { get; set; } = null!;
        public decimal? Price { get; set; }
        public string Status { get; set; } = null!;
    }
}
