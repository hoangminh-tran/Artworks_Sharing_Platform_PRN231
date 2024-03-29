using Artworks_Sharing_Plaform_Api.AppDatabaseContext;
using Artworks_Sharing_Plaform_Api.Enum;
using Artworks_Sharing_Plaform_Api.Model;
using Artworks_Sharing_Plaform_Api.Model.Dto.ReqDto;
using Artworks_Sharing_Plaform_Api.Model.Dto.ResDto;
using Artworks_Sharing_Plaform_Api.Repository.Interface;
using Artworks_Sharing_Plaform_Api.Service.Interface;

namespace Artworks_Sharing_Plaform_Api.Service
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IPostRepository _postRepository;
        private readonly IHelpperService _helperService;
        private readonly IAccountRepository _accountRepository;
        private readonly IArtworkRepository _artworkRepository;

        public CommentService(ICommentRepository commentRepository, IPostRepository postRepository, IHelpperService helperService, IAccountRepository accountRepository, IArtworkRepository artworkRepository)
        {
            _commentRepository = commentRepository;
            _postRepository = postRepository;
            _helperService = helperService;
            _accountRepository = accountRepository;
            _artworkRepository = artworkRepository;
        }

        public async Task<bool> CreateArtworkCommentAsync(CreateArtworkCommentResDto resDto)
        {
            try
            {
                if (!_helperService.IsTokenValid())
                {
                    throw new Exception(ServerErrorEnum.NOT_AUTHENTICATED);
                }
                var account = await _accountRepository.GetAccountByIdAsync(_helperService.GetAccIdFromLogged()) ?? throw new Exception(AccountErrorEnum.ACCOUNT_NOT_FOUND);
                var artwork = await _artworkRepository.GetArtworkByIdAsync(resDto.ArtworkId) ?? throw new Exception("ARTWORK_NOT_FOUND");
                Comment comment = new()
                {
                    AccountId = account.Id,
                    ArtworkId = artwork.Id,
                    Content = resDto.Comment
                };
                return await _commentRepository.CreateCommentAsync(comment);
            } catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> CreatePostCommentAsync(CreatePostCommentResDto resDto)
        {
            try
            {
                if (!_helperService.IsTokenValid())
                {
                    throw new Exception(ServerErrorEnum.NOT_AUTHENTICATED);
                }
                var account = await _accountRepository.GetAccountByIdAsync(_helperService.GetAccIdFromLogged()) ?? throw new Exception(AccountErrorEnum.ACCOUNT_NOT_FOUND);
                var post = await _postRepository.GetPostByIdAsync(resDto.PostId) ?? throw new Exception("POST_NOT_FOUND");
                Comment comment = new()
                {
                    AccountId = account.Id,
                    PostId = post.Id,
                    Content = resDto.Comment
                };
                return await _commentRepository.CreateCommentAsync(comment);
            }          
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> DeleteArtworkCommentAsync(Guid commentId)
        {
            try
            {
                if (!_helperService.IsTokenValid())
                {
                    throw new Exception(ServerErrorEnum.NOT_AUTHENTICATED);
                }
                var account = await _accountRepository.GetAccountByIdAsync(_helperService.GetAccIdFromLogged()) ?? throw new Exception(AccountErrorEnum.ACCOUNT_NOT_FOUND);
                var comment = await _commentRepository.GetCommentByIdAsync(commentId) ?? throw new Exception("COMMENT_NOT_FOUND");
                if (comment.AccountId != account.Id)
                {
                    throw new Exception(ServerErrorEnum.NOT_AUTHORIZED);
                }
                return await _commentRepository.DeleteCommentAsync(comment);
            } catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> DeletePostCommentAsync(Guid commentId)
        {
            try
            {
                if (!_helperService.IsTokenValid())
                {
                    throw new Exception(ServerErrorEnum.NOT_AUTHENTICATED);
                }
                var account = await _accountRepository.GetAccountByIdAsync(_helperService.GetAccIdFromLogged()) ?? throw new Exception(AccountErrorEnum.ACCOUNT_NOT_FOUND);
                var comment = await _commentRepository.GetCommentByIdAsync(commentId) ?? throw new Exception("COMMENT_NOT_FOUND");
                if (comment.AccountId != account.Id)
                {
                    throw new Exception(ServerErrorEnum.NOT_AUTHORIZED);
                }
                return await _commentRepository.DeleteCommentAsync(comment);
            } catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<GetCommentResDto>> GetListCommentByArtworksAsync(Guid artworkId)
        {
            try
            {
                var comments = await _commentRepository.GetListCommentsByArtworkIdAsync(artworkId);
                var account = await _accountRepository.GetAccountByIdAsync(_helperService.GetAccIdFromLoogedNotThrow());
                List<GetCommentResDto> resDtos = new();
                foreach (var comment in comments)
                {
                    var accountComment = await _accountRepository.GetAccountByIdAsync(comment.AccountId) ?? new Account();
                    resDtos.Add(new GetCommentResDto
                    {
                        CommentId = comment.Id.ToString(),
                        AccountName = accountComment.FirstName + " " + accountComment.LastName,
                        CreateDateTime = comment.CreateDateTime,
                        Content = comment.Content,
                        IsCommentByAccount = comment.AccountId == account?.Id
                    });
                }
                return resDtos;
            } catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<GetCommentResDto>> GetListCommentByPostsAsync(Guid postId)
        {
            try
            {
                var comments = await _commentRepository.GetListCommentsByPostIdAsync(postId);
                var account = await _accountRepository.GetAccountByIdAsync(_helperService.GetAccIdFromLoogedNotThrow());
                List<GetCommentResDto> resDtos = new();
                foreach (var comment in comments)
                {
                    var accountComment = await _accountRepository.GetAccountByIdAsync(comment.AccountId) ?? new Account();
                    resDtos.Add(new GetCommentResDto
                    {
                        CommentId = comment.Id.ToString(),
                        AccountName = accountComment.FirstName + " " + accountComment.LastName,
                        CreateDateTime = comment.CreateDateTime,
                        Content = comment.Content,
                        IsCommentByAccount = comment.AccountId == account?.Id
                    });
                }
                return resDtos;
            } catch (Exception)
            {
                throw;
            }
        }
    }
}
