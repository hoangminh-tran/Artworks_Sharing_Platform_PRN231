using Artworks_Sharing_Plaform_Api.AppDatabaseContext;
using Artworks_Sharing_Plaform_Api.Model;
using Artworks_Sharing_Plaform_Api.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Artworks_Sharing_Plaform_Api.Repository
{
    public class StatusRepository : IStatusRepository
    {
        private readonly ArtworksSharingPlaformDatabaseContext _db;
        public StatusRepository(ArtworksSharingPlaformDatabaseContext db)
        {
            _db = db;
        }

        public async Task<Status?> GetStatusByNameAsync(string name)
        {
            try
            {
                return await _db.Statuses.FirstOrDefaultAsync(s => s.StatusName.Equals(name));
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Status?> GetStatusByStatusIDAsync(Guid StatusID)
        {
            try
            {
                return await _db.Statuses.FindAsync(StatusID);
            }
            catch (Exception)
            {                
                throw;
            }
        }

    }
}
