using Artworks_Sharing_Plaform_Api.AppDatabaseContext;
using Artworks_Sharing_Plaform_Api.Model;
using Artworks_Sharing_Plaform_Api.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Artworks_Sharing_Plaform_Api.Repository
{
    public class PostRepository : IPostRepository
    {
        private readonly ArtworksSharingPlaformDatabaseContext _db;

        public PostRepository(ArtworksSharingPlaformDatabaseContext db)
        {
            _db = db;
        }

        public async Task<Post> CreatePostAsync(Post post)
        {
            try
            {
                await _db.Post.AddAsync(post);
                await _db.SaveChangesAsync();
                return post;
            } catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Post>> GetAllPostByGuest()
        {
            try
            {
                // Get List Post where DeleteDateTime == null
                return await _db.Post.Where(x => x.DeleteDateTime == null).ToListAsync();
            } catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Post>> GetListPostAsync()
        {
            try
            {
                return await _db.Post.ToListAsync();
            } catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Post>> GetListPostByCreatorIdAsync(Guid creatorId)
        {
            try
            {
                return await _db.Post.Where(x => x.CreatorId.Equals(creatorId)).ToListAsync();
            } catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Post>> GetListPostByCreatorNameAsync(string creatorName)
        {
            try
            {
                return await _db.Post.Include(p => p.Creator).Where(p => (p.Creator.FirstName + p.Creator.LastName).ToUpper().Contains(creatorName.ToUpper().Trim())).ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Post?> GetPostByIdAsync(Guid postId)
        {
            try
            {
                return await _db.Post.FirstOrDefaultAsync(x => x.Id.Equals(postId));
                
            }catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> UpdatePostAsync(Post post)
        {
            try
            {
                _db.Post.Update(post);
                await _db.SaveChangesAsync();
                return true;
            } catch (Exception)
            {
                throw;
            }
        }
    }
}
