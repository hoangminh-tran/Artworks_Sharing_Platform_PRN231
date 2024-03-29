using Artworks_Sharing_Plaform_Api.AppDatabaseContext;
using Artworks_Sharing_Plaform_Api.Enum;
using Artworks_Sharing_Plaform_Api.Model;
using Artworks_Sharing_Plaform_Api.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Artworks_Sharing_Plaform_Api.Repository
{
    public class LikeByRepository : ILikeByRepository
    {
        private readonly ArtworksSharingPlaformDatabaseContext _db;
        public LikeByRepository(ArtworksSharingPlaformDatabaseContext db)
        {
            _db = db;
        }

        public async Task<bool> CreateLikeAsync(LikeBy likeBy)
        {
            try
            {
                await _db.LikeBys.AddAsync(likeBy);
                await _db.SaveChangesAsync();
                return true;
            } catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> DeleteLikeAsync(LikeBy likeBy)
        {
            try
            {
                _db.LikeBys.Remove(likeBy);
                await _db.SaveChangesAsync();
                return true;
            } catch (Exception)
            {
                throw;
            }
        }

        public async Task<LikeBy?> GetLikeByArtworkIdByCustomerIdAsync(Guid artworkId, Guid customerId)
        {
            try
            {
                return await _db.LikeBys.FirstOrDefaultAsync(l => l.ArtworkId.Equals(artworkId) && l.AccountId.Equals(customerId));
            } catch (Exception)
            {
                throw;
            }
        }

        public Task<LikeBy?> GetLikeByPostIdByCustomerIdAsync(Guid postId, Guid customerId)
        {
            try
            {
                return _db.LikeBys.FirstOrDefaultAsync(l => l.PostId.Equals(postId) && l.AccountId.Equals(customerId));
            } catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<LikeBy>> GetListLikeByArtworkIdAsync(Guid artworkId)
        {
            try
            {
                return await _db.LikeBys.Where(l => l.ArtworkId.Equals(artworkId)).ToListAsync();
            } catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<LikeBy>> GetListLikeByPostIdAsync(Guid postId)
        {
            try
            {
                return await _db.LikeBys.Where(l => l.PostId.Equals(postId)).ToListAsync();
            } catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> LikeArtWorkAsync(LikeBy likeBy)
        {            
            try
            {                
                await _db.LikeBys.AddAsync(likeBy);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> LikeCommentAsync(LikeBy likeBy)
        {            
            try
            {                
                await _db.LikeBys.AddAsync(likeBy);
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
