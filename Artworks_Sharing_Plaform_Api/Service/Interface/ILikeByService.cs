using Artworks_Sharing_Plaform_Api.Model;
using System.Security.Principal;

namespace Artworks_Sharing_Plaform_Api.Service.Interface
{
    public interface ILikeByService
    {
        Task<bool> LikeArtWorkAsync(Guid artworkId);
        Task<bool> LikeCommentAsync(Guid commentId);
        Task<bool> LikePostAsync(Guid postId);
        Task<bool> UnlikeArtWorkAsync(Guid artworkId);
        Task<bool> UnlikeCommentAsync(Guid commentId);
        Task<bool> UnlikePostAsync(Guid postId);
    }
}
