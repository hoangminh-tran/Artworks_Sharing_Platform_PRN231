using Artworks_Sharing_Plaform_Api.Model;

namespace Artworks_Sharing_Plaform_Api.Repository.Interface
{
    public interface ICommentRepository
    {
        Task<Comment?> GetCommentByIdAsync(Guid commentId);
        Task<bool> CreateCommentAsync(Comment comment);
        Task<List<Comment>> GetListCommentsByPostIdAsync(Guid postId);
        Task<List<Comment>> GetListCommentsByArtworkIdAsync(Guid artworkId);
        Task<bool> DeleteCommentAsync(Comment comment);
    }
}
