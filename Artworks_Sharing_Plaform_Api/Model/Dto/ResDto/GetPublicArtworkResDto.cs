namespace Artworks_Sharing_Plaform_Api.Model.Dto.ResDto
{
    public class GetPublicArtworkResDto
    {
        public Guid ArtworkId { get; set; }
        public byte[] Image { get; set; } = null!;
    }
}
