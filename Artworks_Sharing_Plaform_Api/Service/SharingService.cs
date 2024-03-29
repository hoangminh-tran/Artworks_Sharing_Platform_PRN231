using Artworks_Sharing_Plaform_Api.Enum;
using Artworks_Sharing_Plaform_Api.Model;
using Artworks_Sharing_Plaform_Api.Model.Dto.ReqDto;
using Artworks_Sharing_Plaform_Api.Repository.Interface;
using Artworks_Sharing_Plaform_Api.Service.Interface;

namespace Artworks_Sharing_Plaform_Api.Service
{
    public class SharingService : ISharingService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IHelpperService _helperService;
        private readonly ISharingRepository _sharingRepository;
        private readonly IPostArtworkRepository _postArtworkRepository;
        private readonly IPostRepository _postRepository;

        public SharingService(IAccountRepository accountRepository, IHelpperService helperService, ISharingRepository sharingRepository, IPostArtworkRepository postArtworkRepository, IPostRepository postRepository)
        {
            _accountRepository = accountRepository;
            _helperService = helperService;
            _sharingRepository = sharingRepository;
            _postArtworkRepository = postArtworkRepository;
            _postRepository = postRepository;
        }

        public async Task<bool> CreateSharingPostArtwork(SharePostArtworkDto sharingPostDto)
        {
            try
            {
                if (!_helperService.IsTokenValid())
                {
                    throw new Exception(ServerErrorEnum.NOT_AUTHENTICATED);
                }

                var accLoggedId = await _accountRepository.GetAccountByIdAsync(_helperService.GetAccIdFromLogged()) ?? throw new Exception(ServerErrorEnum.NOT_AUTHENTICATED);
                var account = await _accountRepository.GetAccountByIdAsync(accLoggedId.Id);
                if (account == null)
                {
                    throw new Exception(AccountErrorEnum.ACCOUNT_NOT_FOUND);
                }
                
                var post = await _postRepository.GetPostByIdAsync(sharingPostDto.PostId);
                if (post == null)
                {
                    throw new Exception(PostErrorEnum.POST_NOT_FOUND);
                }

                Sharing sharing = new Sharing
                {
                    AccountId = accLoggedId.Id,
                    CreateDateTime = DateTime.UtcNow,
                    Description = sharingPostDto.DescriptionOfSharing ?? "",
                    PostId = sharingPostDto.PostId,
                };
                return await _sharingRepository.CreateSharingPostArtwork(sharing);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
