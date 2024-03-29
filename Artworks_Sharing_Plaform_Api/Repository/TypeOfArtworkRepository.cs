using Artworks_Sharing_Plaform_Api.AppDatabaseContext;
using Artworks_Sharing_Plaform_Api.Model;
using Artworks_Sharing_Plaform_Api.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Artworks_Sharing_Plaform_Api.Repository
{
    public class TypeOfArtworkRepository : ITypeOfArtworkRepository
    {
        private readonly ArtworksSharingPlaformDatabaseContext _db;

        public TypeOfArtworkRepository(ArtworksSharingPlaformDatabaseContext db)
        {
            _db = db;
        }

        public async Task<bool> CreateTypeOfArtwork(TypeOfArtwork typeOfArtwork)
        {
            try
            {
                await _db.TypeOfArtworks.AddAsync(typeOfArtwork);
                await _db.SaveChangesAsync();
                return true;
            } catch (Exception)
            {                
                throw;
            }           
        }

        public async Task<List<TypeOfArtwork>> GetListTypeOfArtworkAsync()
        {
            try
            {
                return await _db.TypeOfArtworks
                    .Include(type => type.Status)
                    .ToListAsync();
            } catch (Exception)
            {
                throw;
            }
        }

        public async Task<TypeOfArtwork?> GetTypeOfArtworkByIdAsync(Guid id)
        {
            try
            {
                return await _db.TypeOfArtworks
                     .Include(type => type.Status)
                     .FirstOrDefaultAsync(type => type.Id == id);
            } catch (Exception)
            {                
                throw;
            }
        }

        public async Task<bool> UpdateTypeOfArtwork(TypeOfArtwork typeOfArtwork)
        {
            try
            {
                _db.TypeOfArtworks.Update(typeOfArtwork);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<TypeOfArtwork>> GetListTypeOfArtworkByTypeOfArtworkNameAsync(string type)
        {
            try
            {
                return await _db.TypeOfArtworks
                    .Where(typeOfArtwork => typeOfArtwork.Type.ToUpper().Contains(type.ToUpper()))
                    .Include(typeOfArtwork => typeOfArtwork.Status)
                    .ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<TypeOfArtwork> GetTypeOfArtworkByIdAndStatusAsync(Guid id, Guid statusId)
        {
            try
            {
                return await _db.TypeOfArtworks
                    .Include(typeOfArtwork => typeOfArtwork.Status)
                    .FirstOrDefaultAsync(typeOfArtwork => typeOfArtwork.Id == id && typeOfArtwork.StatusId == statusId);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
