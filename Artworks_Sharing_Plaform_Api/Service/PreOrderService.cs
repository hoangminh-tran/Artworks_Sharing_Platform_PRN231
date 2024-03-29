using Artworks_Sharing_Plaform_Api.Enum;
using Artworks_Sharing_Plaform_Api.Model;
using Artworks_Sharing_Plaform_Api.Model.Dto.ResDto;
using Artworks_Sharing_Plaform_Api.Repository.Interface;
using Artworks_Sharing_Plaform_Api.Service.Interface;

namespace Artworks_Sharing_Plaform_Api.Service
{
    public class PreOrderService : IPreOrderService
    {
        private readonly IPreOrderRepository _preOrderRepository;
        private readonly IHelpperService _helperService;
        private readonly IArtworkRepository _artworkRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IStatusRepository _statusRepository;

        public PreOrderService(IPreOrderRepository preOrderRepository, IHelpperService helperService, IArtworkRepository artworkRepository, IAccountRepository accountRepository, IStatusRepository statusRepository)
        {
            _preOrderRepository = preOrderRepository;
            _helperService = helperService;
            _artworkRepository = artworkRepository;
            _accountRepository = accountRepository;
            _statusRepository = statusRepository;
        }

        public async Task<bool> CreatePreOrderAsync(Guid artworkId)
        {
            try
            {
                if (!_helperService.IsTokenValid())
                {
                    throw new Exception(ServerErrorEnum.NOT_AUTHENTICATED);
                }
                var accLoggedId = await _accountRepository.GetAccountByIdAsync(_helperService.GetAccIdFromLogged()) ?? throw new Exception(ServerErrorEnum.NOT_AUTHENTICATED);
                var artwork = await _artworkRepository.GetArtworkByArtworkByIdAsync(artworkId) ?? throw new Exception("ARTWORK_NOT_FOUND");
                var status = await _statusRepository.GetStatusByNameAsync("PENDING") ?? throw new Exception("STATUS_NOT_FOUND");
                // Check if the pre-order already exists
                var preOrder = await _preOrderRepository.GetPreOrderByArtworkIdAndCustomerIdAsync(artworkId, accLoggedId.Id);
                if (preOrder != null)
                {
                    throw new Exception("PRE_ORDER_ALREADY_EXISTS");
                }                
                PreOrder newPreOrder = new()
                {
                    ArtworkId = artwork.Id,
                    CustomerId = accLoggedId.Id,
                    StatusId = status.Id,
                };
                return await _preOrderRepository.CreatePreOrderAsync(newPreOrder);
            } catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<GetPreOrderByCustomerResDto>> GetListPreOrderByCustomerIdAsync()
        {
            try
            {
                if (!_helperService.IsTokenValid())
                {
                    throw new Exception(ServerErrorEnum.NOT_AUTHENTICATED);
                }
                var accLoggedId = await _accountRepository.GetAccountByIdAsync(_helperService.GetAccIdFromLogged()) ?? throw new Exception(ServerErrorEnum.NOT_AUTHENTICATED);
                var listPreOrder = await _preOrderRepository.GetListPreOrderByCustomerIdAsync(accLoggedId.Id);
                List<GetPreOrderByCustomerResDto> listPreOrderRes = new();
                foreach (var preOrder in listPreOrder)
                {
                    var artwork = await _artworkRepository.GetArtworkByArtworkByIdAsync(preOrder.ArtworkId) ?? throw new Exception("ARTWORK_NOT_FOUND");
                    var creator = await _accountRepository.GetAccountByIdAsync(artwork.CreatorId) ?? throw new Exception("CREATOR_NOT_FOUND");
                    var status = await _statusRepository.GetStatusByStatusIDAsync(preOrder.StatusId) ?? throw new Exception("STATUS_NOT_FOUND");
                    listPreOrderRes.Add(new GetPreOrderByCustomerResDto
                    {
                        ArtworkName = artwork.Name,
                        ArtworkId = artwork.Id,
                        CreateDateTime = preOrder.CreateDateTime,
                        CreatorName = creator.FirstName + " " + creator.LastName,
                        Description = artwork.Description,
                        Image = artwork.Image,
                        PreOrderId = preOrder.Id,
                        Price = artwork.Price ?? 0,
                        StatusName = status.StatusName,
                        IsSold = artwork.OrderId != null
                    });
                }
                return listPreOrderRes;
            } catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> DeletePreOrderAsync(Guid preOrderId)
        {
            try
            {
                if (!_helperService.IsTokenValid())
                {
                    throw new Exception(ServerErrorEnum.NOT_AUTHENTICATED);
                }
                var preOrder = await _preOrderRepository.GetPreOrderByIdAsync(preOrderId) ?? throw new Exception("PRE_ORDER_NOT_FOUND");
                if (preOrder.CustomerId != _helperService.GetAccIdFromLogged())
                {
                    throw new Exception(ServerErrorEnum.NOT_AUTHORIZED);
                }
                return await _preOrderRepository.DeletePreOrderAsync(preOrder);
            } catch (Exception)
            {
                throw;
            }
        }
    }
}
