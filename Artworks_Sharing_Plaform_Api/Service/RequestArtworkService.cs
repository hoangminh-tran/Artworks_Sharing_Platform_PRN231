using Artworks_Sharing_Plaform_Api.Enum;
using Artworks_Sharing_Plaform_Api.Model;
using Artworks_Sharing_Plaform_Api.Repository.Interface;
using Artworks_Sharing_Plaform_Api.Service.Interface;

namespace Artworks_Sharing_Plaform_Api.Service
{
    public class RequestArtworkService : IRequestArtworkService
    {
        private readonly IRequestArtworkRepository _requestArtworkRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IHelpperService _helperService;
        private readonly IStatusService _statusService;


        public RequestArtworkService(IAccountRepository accountRepository, IRoleRepository roleRepository, IHelpperService helperService, IRequestArtworkRepository requestArtworkrepository, IStatusService statusService)
        {
            _accountRepository = accountRepository;
            _roleRepository = roleRepository;
            _helperService = helperService;
            _requestArtworkRepository = requestArtworkrepository;
            _statusService = statusService;
        }

        public async Task<string> AcceptOrRejectRequestArtwork(bool isAccept, Guid requestArtworkId)
        {
            try
            {
                if (!_helperService.IsTokenValid())
                {
                    throw new Exception(ServerErrorEnum.NOT_AUTHENTICATED);
                }
                var accLoggedId = await _accountRepository.GetAccountByIdAsync(_helperService.GetAccIdFromLogged()) ?? throw new Exception(ServerErrorEnum.NOT_AUTHENTICATED);
                var roleCreator = await _roleRepository.GetRoleByNameAsync(RoleEnum.CREATOR) ?? throw new Exception(ServerErrorEnum.SERVER_ERROR);
                if (accLoggedId.RoleId != roleCreator.Id)
                {
                    throw new Exception(ServerErrorEnum.NOT_AUTHORIZED);
                }

                var result = "";
                var requestArtwork = await _requestArtworkRepository.GetRequestArtworkByRequestArtworkId(requestArtworkId);
                if (requestArtwork != null)
                {
                    if (isAccept)
                    {
                        var acceptStatus = await _statusService.GetStatusByStatusName("ACCEPTED");
                        requestArtwork.StatusId = acceptStatus!.Id;
                        await _requestArtworkRepository.AcceptOrRejectRequestArtwork(requestArtwork);
                        result = "ACCEPTED";
                    }
                    else
                    {
                        var acceptStatus = await _statusService.GetStatusByStatusName("REJECTED");
                        requestArtwork.StatusId = acceptStatus!.Id;
                        await _requestArtworkRepository.AcceptOrRejectRequestArtwork(requestArtwork);
                        result = "REJECTED";
                    }
                }
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<RequestArtwork> GetRequestArtworkByRequestArtworkId(Guid requestArtworkId)
        {
            try
            {
                if (!_helperService.IsTokenValid())
                {
                    throw new Exception(ServerErrorEnum.NOT_AUTHENTICATED);
                }
                var accLoggedId = await _accountRepository.GetAccountByIdAsync(_helperService.GetAccIdFromLogged()) ?? throw new Exception(ServerErrorEnum.NOT_AUTHENTICATED);
                var roleCreator = await _roleRepository.GetRoleByNameAsync(RoleEnum.CREATOR) ?? throw new Exception(ServerErrorEnum.SERVER_ERROR);
                if (accLoggedId.RoleId != roleCreator.Id)
                {
                    throw new Exception(ServerErrorEnum.NOT_AUTHORIZED);
                }
                return await _requestArtworkRepository.GetRequestArtworkByRequestArtworkId(requestArtworkId) ?? throw new Exception("REQUEST_ARTWORK_NOT_FOUND");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
