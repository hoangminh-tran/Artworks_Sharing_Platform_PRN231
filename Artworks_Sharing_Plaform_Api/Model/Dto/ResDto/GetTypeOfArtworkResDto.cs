namespace Artworks_Sharing_Plaform_Api.Model.Dto.ResDto
{
    public class GetTypeOfArtworkResDto
    {
        public Guid Id { get; set; }
        public string Type { get; set; } = null!;
        public string TypeDescription { get; set; } = null!;
        public string? statusName { get; set; }
    }
}
