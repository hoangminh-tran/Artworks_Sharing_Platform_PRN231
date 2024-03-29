using Artworks_Sharing_Plaform_Api.Model;

namespace Artworks_Sharing_Plaform_Api.Repository.Interface
{
    public interface IUserFollowerRepository
    {
        Task<bool> FollowAsync(UserFollow follow);
        Task<bool> UnfollowAsync(UserFollow follow);
    }
}
