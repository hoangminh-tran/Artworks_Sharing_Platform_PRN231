using Artworks_Sharing_Plaform_Api.AppDatabaseContext;
using Artworks_Sharing_Plaform_Api.Model;
using Artworks_Sharing_Plaform_Api.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Artworks_Sharing_Plaform_Api.Repository
{
    public class ComplantRepository : IComplantRepository
    {
        private readonly ArtworksSharingPlaformDatabaseContext _db;
        public ComplantRepository(ArtworksSharingPlaformDatabaseContext db)
        {
            _db = db;
        }        
        public async Task<bool> CreateComplantAsync(Complant complant)

        {
            try
            {
                await _db.Complants.AddAsync(complant);
                _db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Complant?> GetComplantByComplantIDAsync(Guid ComplantID)
        {
            try
            {
                return await _db.Complants.FindAsync(ComplantID);
            }
            catch (Exception)
            {                
                throw;
            }
        }
        public async Task<bool> UpdateComplaintAsync(Complant complant)
        {
            try
            {
                _db.Complants.Update(complant);
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
