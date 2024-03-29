using Artworks_Sharing_Plaform_Api.Model;

namespace Artworks_Sharing_Plaform_Api.Repository.Interface
{
    public interface ILikeByRepository
    {
        Task<bool> LikeArtWorkAsync(LikeBy likeBy);
        Task<bool> LikeCommentAsync(LikeBy likeBy);
        Task<List<LikeBy>> GetListLikeByArtworkIdAsync(Guid artworkId);
        Task<List<LikeBy>> GetListLikeByPostIdAsync(Guid postId);
        Task<LikeBy?> GetLikeByArtworkIdByCustomerIdAsync(Guid artworkId, Guid customerId);
        Task<LikeBy?> GetLikeByPostIdByCustomerIdAsync(Guid postId, Guid customerId);
        Task<bool> DeleteLikeAsync(LikeBy likeBy);
        Task<bool> CreateLikeAsync(LikeBy likeBy);
    }
}
