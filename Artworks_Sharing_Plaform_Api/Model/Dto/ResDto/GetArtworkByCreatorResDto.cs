namespace Artworks_Sharing_Plaform_Api.Model.Dto.ResDto
{
    public class GetArtworkByCreatorResDto
    {
        public Guid ArtworkId { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public byte[] Image { get; set; } = null!;
        public List<GetTypeOfArtworkResDto> TypeOfArtworks { get; set; } = null!;
        public string StatusName { get; set; } = null!;
        public DateTime CreateDateTime { get; set; }
        public string UserOwnerName { get; set; } = null!;
        public decimal Price { get; set; }
    }
}
