using Artworks_Sharing_Plaform_Api.Model.Dto.ReqDto;
using Artworks_Sharing_Plaform_Api.Model.Dto.ResDto;

namespace Artworks_Sharing_Plaform_Api.Service.Interface
{
    public interface ICommentService
    {
        Task<bool> CreatePostCommentAsync(CreatePostCommentResDto resDto);
        Task<bool> CreateArtworkCommentAsync(CreateArtworkCommentResDto resDto);
        Task<List<GetCommentResDto>> GetListCommentByPostsAsync(Guid postId);
        Task<List<GetCommentResDto>> GetListCommentByArtworksAsync(Guid artworkId);
        Task<bool> DeletePostCommentAsync(Guid commentId);
        Task<bool> DeleteArtworkCommentAsync(Guid commentId);
    }
}
