using Artworks_Sharing_Plaform_Api.Model.Dto.ReqDto;

namespace Artworks_Sharing_Plaform_Api.Service.Interface
{
    public interface IUserFollowerService
    {
        Task<bool> FollowUserAsync(FollowDto follow);
        Task<bool> UnfollowUserAsync(FollowDto follow);

    }
}