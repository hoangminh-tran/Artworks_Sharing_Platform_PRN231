namespace Artworks_Sharing_Plaform_Api.Model.Dto.ResDto
{
    public class GetPreOrderByCustomerResDto
    {
        public Guid PreOrderId { get; set; }
        public Guid ArtworkId { get; set; }
        public string ArtworkName { get; set; } = null!;
        public string Description { get; set; } = null!;
        public byte[] Image { get; set; } = null!;
        public string CreatorName { get; set; } = null!;
        public string StatusName { get; set; } = null!;
        public decimal Price { get; set; }
        public DateTime CreateDateTime { get; set; }
        public bool IsSold { get; set; }
    }
}
