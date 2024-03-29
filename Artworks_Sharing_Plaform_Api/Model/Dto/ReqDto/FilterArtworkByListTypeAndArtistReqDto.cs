namespace Artworks_Sharing_Plaform_Api.Model.Dto.ReqDto
{
    public class FilterArtworkByListTypeAndArtistReqDto
    {
        public List<Guid>? TypeOfArtworkIds {  get; set; }
        public Guid? ArtistId { get; set; }
    }
}
