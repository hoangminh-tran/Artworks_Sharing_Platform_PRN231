using Artworks_Sharing_Plaform_Api.Model.Abstract;

namespace Artworks_Sharing_Plaform_Api.Model
{
    public class PreOrder : Common
    {
        public Guid CustomerId { get; set; }
        public Account? Account { get; set; }
        public Guid ArtworkId { get; set; }
        public Artwork? Artwork { get; set; }
        public Guid StatusId { get; set; }
        public Status? Status { get; set; }
    }
}
