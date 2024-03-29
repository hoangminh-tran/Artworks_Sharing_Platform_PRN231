using Artworks_Sharing_Plaform_Api.Model;

namespace Artworks_Sharing_Plaform_Api.Repository.Interface
{
    public interface IPostRepository
    {
        Task<Post?> GetPostByIdAsync(Guid postId);
        Task<List<Post>> GetListPostAsync();
        Task<Post> CreatePostAsync(Post post);
        Task<bool> UpdatePostAsync(Post post);
        Task<List<Post>> GetListPostByCreatorNameAsync(string creatorName);
        Task<List<Post>> GetAllPostByGuest();
        Task<List<Post>> GetListPostByCreatorIdAsync(Guid creatorId);
    }
}
