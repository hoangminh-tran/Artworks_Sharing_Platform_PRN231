using Artworks_Sharing_Plaform_Api.Enum;
using Artworks_Sharing_Plaform_Api.Model;
using Artworks_Sharing_Plaform_Api.Model.Dto.ReqDto;
using Artworks_Sharing_Plaform_Api.Repository.Interface;
using Artworks_Sharing_Plaform_Api.Service.Interface;

namespace Artworks_Sharing_Plaform_Api.Service
{
    public class UserFollowerService : IUserFollowerService
    {
        private readonly IUserFollowerRepository _userFollowerRepository;
        private readonly IAccountRepository _accountRepository;        
        private readonly IHelpperService _helperService;

        public UserFollowerService(IUserFollowerRepository userFollowerRepository, IAccountRepository accountRepository, IHelpperService helperService)
        {
            _userFollowerRepository = userFollowerRepository;
            _accountRepository = accountRepository;            
            _helperService = helperService;
        }

        public async Task<bool> FollowUserAsync(FollowDto follow)
        {
            try
            {
                if (!_helperService.IsTokenValid())
                {
                    throw new Exception(ServerErrorEnum.NOT_AUTHENTICATED);
                }
                var accLoggedId = await _accountRepository.GetAccountByIdAsync(_helperService.GetAccIdFromLogged()) ?? throw new Exception(ServerErrorEnum.NOT_AUTHENTICATED);                
                var creatorUser = await _accountRepository.GetAccountByIdAsync(follow.CreatorId) ?? throw new Exception(UserFollowerErrorEnum.CREATOR_USER_NOT_FOUND);
                UserFollow userFollower = new()
                {
                    UserId = accLoggedId.Id,
                    FollowingId = creatorUser.Id,
                };
                return await _userFollowerRepository.FollowAsync(userFollower);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> UnfollowUserAsync(FollowDto follow)
        {
            try
            {
                if (!_helperService.IsTokenValid())
                {
                    throw new Exception(ServerErrorEnum.NOT_AUTHENTICATED);
                }
                var accLoggedId = await _accountRepository.GetAccountByIdAsync(_helperService.GetAccIdFromLogged()) ?? throw new Exception(ServerErrorEnum.NOT_AUTHENTICATED);                
                var creatorUser = await _accountRepository.GetAccountByIdAsync(follow.CreatorId) ?? throw new Exception(UserFollowerErrorEnum.CREATOR_USER_NOT_FOUND);
                UserFollow userFollower = new()
                {
                    UserId = accLoggedId.Id,
                    FollowingId = creatorUser.Id,
                };
                return await _userFollowerRepository.UnfollowAsync(userFollower);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
