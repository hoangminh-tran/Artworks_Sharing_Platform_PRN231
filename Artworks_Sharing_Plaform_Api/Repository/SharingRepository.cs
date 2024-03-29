using Artworks_Sharing_Plaform_Api.AppDatabaseContext;
using Artworks_Sharing_Plaform_Api.Model;
using Artworks_Sharing_Plaform_Api.Repository.Interface;

namespace Artworks_Sharing_Plaform_Api.Repository
{
    public class SharingRepository : ISharingRepository
    {
        private readonly ArtworksSharingPlaformDatabaseContext _db;

        public SharingRepository(ArtworksSharingPlaformDatabaseContext db)
        {
            _db = db;
        }

        public async Task<bool> CreateSharingPostArtwork(Sharing sharing)
        {
            try
            {
                _db.Sharings.Add(sharing);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
