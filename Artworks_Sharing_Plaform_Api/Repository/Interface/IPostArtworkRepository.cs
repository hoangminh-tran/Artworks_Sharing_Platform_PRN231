using Artworks_Sharing_Plaform_Api.Model;

namespace Artworks_Sharing_Plaform_Api.Repository.Interface
{
    public interface IPostArtworkRepository
    {
        Task<PostArtwork?> GetPostByIdAsync(Guid postId);
        Task<List<PostArtwork>> GetListPostArtworkByPostIdAsync(Guid postId);
        Task<PostArtwork> CreatePostArtworkAsync(PostArtwork postArtwork);

    }
}
