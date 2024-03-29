using Artworks_Sharing_Plaform_Api.AppDatabaseContext;
using Artworks_Sharing_Plaform_Api.Model;
using Artworks_Sharing_Plaform_Api.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Artworks_Sharing_Plaform_Api.Repository
{
    public class PostArtworkRepository : IPostArtworkRepository
    {
        private readonly ArtworksSharingPlaformDatabaseContext _db;

        public PostArtworkRepository(ArtworksSharingPlaformDatabaseContext db)
        {
            _db = db;
        }

        public async Task<PostArtwork> CreatePostArtworkAsync(PostArtwork postArtwork)
        {
            try
            {
                await _db.PostArtworks.AddAsync(postArtwork);
                await _db.SaveChangesAsync();
                return postArtwork;
            } catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<PostArtwork>> GetListPostArtworkByPostIdAsync(Guid postId)
        {
            try
            {
                return await _db.PostArtworks.Where(p => p.PostId.Equals(postId)).ToListAsync();
            } catch (Exception)
            {
                throw;
            }
        }

        public async Task<PostArtwork?> GetPostByIdAsync(Guid postId)
        {
            try
            {
                return await _db.PostArtworks.FirstOrDefaultAsync(p => p.PostId.Equals(postId));                
            }catch (Exception)
            {
                throw;
            }
        }
    }
}
