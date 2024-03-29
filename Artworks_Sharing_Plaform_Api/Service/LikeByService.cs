using Artworks_Sharing_Plaform_Api.Enum;
using Artworks_Sharing_Plaform_Api.Model;
using Artworks_Sharing_Plaform_Api.Repository.Interface;
using Artworks_Sharing_Plaform_Api.Service.Interface;
using System.ComponentModel.Design;

namespace Artworks_Sharing_Plaform_Api.Service
{
    public class LikeByService : ILikeByService
    {
        private readonly ILikeByRepository _likeByRepository;
        private readonly IArtworkRepository _artworkRepository;
        private readonly IAccountRepository _accountRepository;              
        private readonly IHelpperService _helperService;
        private readonly ICommentRepository _commentRepository;
        private readonly IPostArtworkRepository _postArtworkRepository;

        public LikeByService(ILikeByRepository likeByRepository, IArtworkRepository artworkRepository, IAccountRepository accountRepository, IHelpperService helperService, ICommentRepository commentRepository, IPostArtworkRepository postArtworkRepository)
        {
            _likeByRepository = likeByRepository;
            _artworkRepository = artworkRepository;
            _accountRepository = accountRepository;
            _helperService = helperService;
            _commentRepository = commentRepository;
            _postArtworkRepository = postArtworkRepository;
        }

        public async Task<bool> LikeArtWorkAsync(Guid artworkId)
        {
            if (!_helperService.IsTokenValid())
            {
                throw new Exception(ServerErrorEnum.NOT_AUTHENTICATED);
            }
            var accLoggedId = await _accountRepository.GetAccountByIdAsync(_helperService.GetAccIdFromLogged()) ?? throw new Exception(ServerErrorEnum.NOT_AUTHENTICATED);            
            _ = await _artworkRepository.GetArtworkByIdAsync(artworkId) ?? throw new Exception(ArtWorkErrorEnum.ARTWORK_NOT_FOUND);

            //create new object likeby
            var likeBy = new LikeBy
            {
                AccountId = accLoggedId.Id,
                ArtworkId = artworkId,
                CreateDateTime = DateTime.Now               
            };
            return await _likeByRepository.LikeArtWorkAsync(likeBy);
        }

        public async Task<bool> LikeCommentAsync(Guid commentId)
        {
            if (!_helperService.IsTokenValid())
            {
                throw new Exception(ServerErrorEnum.NOT_AUTHENTICATED);
            }

            var accLoggedId = await _accountRepository.GetAccountByIdAsync(_helperService.GetAccIdFromLogged()) ?? throw new Exception(ServerErrorEnum.NOT_AUTHENTICATED);           
            _ = await _commentRepository.GetCommentByIdAsync(commentId) ?? throw new Exception(CommentErrorEnum.COMMENT_NOT_FOUND);

            //create new object likeby
            var likeBy = new LikeBy
            {
                AccountId = accLoggedId.Id,
                CommentId = commentId,
                CreateDateTime = DateTime.Now                
            };
            return await _likeByRepository.LikeCommentAsync(likeBy);
        }

        public async Task<bool> LikePostAsync(Guid postId)
        {
            try
            {
                if (!_helperService.IsTokenValid())
                {
                    throw new Exception(ServerErrorEnum.NOT_AUTHENTICATED);
                }
                var accLoggedId = await _accountRepository.GetAccountByIdAsync(_helperService.GetAccIdFromLogged()) ?? throw new Exception(ServerErrorEnum.NOT_AUTHENTICATED);
                _ = await _postArtworkRepository.GetPostByIdAsync(postId) ?? throw new Exception(PostArtworkErrorEnum.POST_ARTWORK_NOT_FOUND);
                LikeBy like = new()
                {
                    AccountId = accLoggedId.Id,
                    PostId = postId                    
                };
                return await _likeByRepository.CreateLikeAsync(like);
            } catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> UnlikeArtWorkAsync(Guid artworkId)
        {
            try
            {
                if (!_helperService.IsTokenValid())
                {
                    throw new Exception(ServerErrorEnum.NOT_AUTHENTICATED);
                }
                var accLoggedId = await _accountRepository.GetAccountByIdAsync(_helperService.GetAccIdFromLogged()) ?? throw new Exception(ServerErrorEnum.NOT_AUTHENTICATED);
                _ = await _artworkRepository.GetArtworkByIdAsync(artworkId) ?? throw new Exception(ArtWorkErrorEnum.ARTWORK_NOT_FOUND);
                var likeBy = await _likeByRepository.GetLikeByArtworkIdByCustomerIdAsync(artworkId, accLoggedId.Id);
                if (likeBy == null)
                {
                    throw new Exception("Like not found");
                }
                return await _likeByRepository.DeleteLikeAsync(likeBy);
            } catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> UnlikeCommentAsync(Guid commentId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UnlikePostAsync(Guid postId)
        {
            try
            {
                if (!_helperService.IsTokenValid())
                {
                    throw new Exception(ServerErrorEnum.NOT_AUTHENTICATED);
                }
                var accLoggedId = await _accountRepository.GetAccountByIdAsync(_helperService.GetAccIdFromLogged()) ?? throw new Exception(ServerErrorEnum.NOT_AUTHENTICATED);
                _ = await _postArtworkRepository.GetPostByIdAsync(postId) ?? throw new Exception(CommentErrorEnum.COMMENT_NOT_FOUND);
                var likeBy = await _likeByRepository.GetLikeByPostIdByCustomerIdAsync(postId, accLoggedId.Id);
                if (likeBy == null)
                {
                    throw new Exception("Like not found");
                }
                return await _likeByRepository.DeleteLikeAsync(likeBy);
            } catch (Exception)
            {
                throw;
            }
        }
    }
}
