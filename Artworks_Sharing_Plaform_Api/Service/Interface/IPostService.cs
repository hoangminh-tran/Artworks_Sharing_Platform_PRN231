using Artworks_Sharing_Plaform_Api.Model;
using Artworks_Sharing_Plaform_Api.Model.Dto.ReqDto;
using Artworks_Sharing_Plaform_Api.Model.Dto.ResDto;

namespace Artworks_Sharing_Plaform_Api.Service.Interface
{
    public interface IPostService
    {
        Task<GetPostResDto> GetPostByIdAsync(Guid postId);
        Task<List<GetPostResDto>> GetListPostAsync();
        Task<bool> CreatePostAsync(CreatePostReqDto newPost);
        Task<List<GetPostResDto>> GetListPostByCreatorNameByCustomerAsync(string creatorName);
        Task<List<GetPostResDto>> GetListPostByCustomerAsync();
        Task<List<GetPostResDto>> GetListPostByCreatorIdAsync(Guid creatorId);
        Task<bool> UpdatePostAsync(PostRequestDto postRequest);
    }
}
