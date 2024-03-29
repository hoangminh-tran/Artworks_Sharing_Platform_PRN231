using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Artworks_Sharing_Plaform_Api.Model.Dto.ResDto
{
    public class GetListHistoryTransactionArtworkResponseDto
    {
        public string CreatorFirstName { get; set; } = null!;

        public string CreatorLastName { get; set; } = null!;

        public string ArtworkName { get; set; } = null!;

        public string OwnerFirstName { get; set; } = null!;

        public string OwnerLastName { get; set; } = null!;

        public decimal? Price { get; set; }

        public DateTime CreateDateTime { get; set; } = DateTime.Now;

        public DateTime? UpdateDateTime { get; set; } = null;

        public DateTime? DeleteDateTime { get; set; } = null;
    }
}
