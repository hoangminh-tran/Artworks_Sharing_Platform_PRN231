namespace Artworks_Sharing_Plaform_Api.Model.Dto.ResDto
{
    public class GetCreatorResDto
    {
        public Guid CreatorId { get; set; }
        public string CreatorFirstName { get; set; } = null!;
        public string CreatorLastName { get; set; } = null!;        
    }
}
