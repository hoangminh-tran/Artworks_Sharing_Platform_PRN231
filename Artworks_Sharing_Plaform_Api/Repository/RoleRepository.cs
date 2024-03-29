using Artworks_Sharing_Plaform_Api.AppDatabaseContext;
using Artworks_Sharing_Plaform_Api.Model;
using Artworks_Sharing_Plaform_Api.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Artworks_Sharing_Plaform_Api.Repository
{
    public class RoleRepository : IRoleRepository
    {
        private readonly ArtworksSharingPlaformDatabaseContext _db;
        public RoleRepository(ArtworksSharingPlaformDatabaseContext db) 
        {
            _db = db;
        }

        public async Task<Role?> GetRoleByNameAsync(string name)
        {
            try
            {
                return await _db.Roles.FirstOrDefaultAsync(r => r.RoleName.Equals(name));
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Role?> GetRoleByRoleIDAsync(Guid ID)
        {
            try
            {
                return await _db.Roles.FindAsync(ID);
            }
            catch (Exception)
            {                
                throw;
            }
        }
    }
}
