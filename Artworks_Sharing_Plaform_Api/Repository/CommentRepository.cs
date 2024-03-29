using Artworks_Sharing_Plaform_Api.AppDatabaseContext;
using Artworks_Sharing_Plaform_Api.Enum;
using Artworks_Sharing_Plaform_Api.Model;
using Artworks_Sharing_Plaform_Api.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Artworks_Sharing_Plaform_Api.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ArtworksSharingPlaformDatabaseContext _db;
        public CommentRepository(ArtworksSharingPlaformDatabaseContext db)
        {
            _db = db;
        }

        public async Task<bool> CreateCommentAsync(Comment comment)
        {
            try
            {
                await _db.Comments.AddAsync(comment);
                await _db.SaveChangesAsync();
                return true;
            } catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> DeleteCommentAsync(Comment comment)
        {
            try
            {
                _db.Comments.Remove(comment);
                await _db.SaveChangesAsync();
                return true;
            } catch (Exception)
            {
                throw;
            }
        }

        public async Task<Comment?> GetCommentByIdAsync(Guid commentId)
        {
            try
            {
                return await _db.Comments.FirstOrDefaultAsync(c => c.Id.Equals(commentId));             
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Comment>> GetListCommentsByArtworkIdAsync(Guid artworkId)
        {
            try
            {
                return await _db.Comments.Where(c => c.ArtworkId.Equals(artworkId)).ToListAsync();
            } catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Comment>> GetListCommentsByPostIdAsync(Guid postId)
        {
            try
            {
                return await _db.Comments.Where(c => c.PostId.Equals(postId)).ToListAsync();
            } catch (Exception)
            {
                throw;
            }
        }
    }
}
