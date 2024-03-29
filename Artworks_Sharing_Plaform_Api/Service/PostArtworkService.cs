using Artworks_Sharing_Plaform_Api.Enum;
using Artworks_Sharing_Plaform_Api.Model;
using Artworks_Sharing_Plaform_Api.Repository.Interface;
using Artworks_Sharing_Plaform_Api.Service.Interface;

namespace Artworks_Sharing_Plaform_Api.Service
{
    public class PostArtworkService : IPostArtworkService
    {
        private readonly IPostArtworkRepository _postArtworkRepository;

        public PostArtworkService(IPostArtworkRepository postArtworkRepository)
        {
            _postArtworkRepository = postArtworkRepository;
        }
        public async Task<PostArtwork> GetPostArtworkByIdAsync(Guid postId)
        {
            try
            {
                var postArtwork = await _postArtworkRepository.GetPostByIdAsync(postId);
                if (postArtwork == null)
                {
                    throw new Exception(PostArtworkErrorEnum.POST_ARTWORK_NOT_FOUND);
                }
                return postArtwork;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
