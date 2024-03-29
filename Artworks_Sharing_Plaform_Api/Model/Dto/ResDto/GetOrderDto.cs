namespace Artworks_Sharing_Plaform_Api.Model.Dto.ResDto;

public class GetOrderDto
{
    public Guid OrderId { get; set; }
    public string? Payment { get; set; }
    public string? UserName { get; set; }
    public string? UserEmail { get; set; }
    public Guid AccountId { get; set; }
    public ICollection<string>? ListNameArtwork { get; set; }
    public ICollection<GetArtworkDto>? Artworks { get; set; }
}
