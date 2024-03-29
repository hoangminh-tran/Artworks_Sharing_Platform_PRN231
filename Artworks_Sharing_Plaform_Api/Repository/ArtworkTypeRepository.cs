using Artworks_Sharing_Plaform_Api.AppDatabaseContext;
using Artworks_Sharing_Plaform_Api.Model;
using Artworks_Sharing_Plaform_Api.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using static Artworks_Sharing_Plaform_Api.Enum.SucessfullyEnum;

namespace Artworks_Sharing_Plaform_Api.Repository
{
    public class ArtworkTypeRepository : IArtworkTypeRepository
    {
        private readonly ArtworksSharingPlaformDatabaseContext _db;

        public ArtworkTypeRepository(ArtworksSharingPlaformDatabaseContext db)
        {
            _db = db;
        }

        public async Task<bool> CreateArtworkTypeAsync(ArtworkType artworkType)
        {
            try
            {
                await _db.ArtworkTypes.AddAsync(artworkType);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {                
                throw;
            }            
        }

        public async Task<bool> UpdateArtworkTypeAsync(ArtworkType artworkType)
        {
            try
            {
                _db.ArtworkTypes.Update(artworkType);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {                
                throw;
            }            
        }

        public async Task<List<ArtworkType>> GetAllArtworkTypeByArtworkIdAsync(Guid artworkId)
        {
            try
            {
                return await _db.ArtworkTypes
                    .Where(at => at.ArtworkId == artworkId)
                    .Include(at => at.Artwork)
                    .Include(at => at.TypeOfArtwork)
                    .ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<List<ArtworkType>> GetArtworkTypeAsyncByTypeOfArtworkId(Guid typeOfArtworkId)
        {
            try
            {
                return await _db.ArtworkTypes
                    .Where(at => at.TypeOfArtworkId == typeOfArtworkId)
                    .Include(at => at.Artwork)
                    .Include(at => at.TypeOfArtwork)
                    .ToListAsync();
            }
            catch (Exception)
            {                
                throw;
            }
        }
    }
}
