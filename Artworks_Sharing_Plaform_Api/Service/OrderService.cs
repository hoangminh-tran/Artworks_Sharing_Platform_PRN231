using Artworks_Sharing_Plaform_Api.Enum;
using Artworks_Sharing_Plaform_Api.Model;
using Artworks_Sharing_Plaform_Api.Model.Dto.ResDto;
using Artworks_Sharing_Plaform_Api.Repository.Interface;
using Artworks_Sharing_Plaform_Api.Service.Interface;

namespace Artworks_Sharing_Plaform_Api.Service
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IArtworkRepository _artworkRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IHelpperService _helperService;
        private readonly IPreOrderRepository _preOrderRepository;
        private readonly IStatusRepository _statusRepository;

        public OrderService(IOrderRepository orderRepository, IArtworkRepository artworkRepository, IAccountRepository accountRepository, IHelpperService helperService, IPreOrderRepository preOrderRepository, IStatusRepository statusRepository)
        {
            _orderRepository = orderRepository;
            _artworkRepository = artworkRepository;
            _accountRepository = accountRepository;
            _helperService = helperService;
            _preOrderRepository = preOrderRepository;
            _statusRepository = statusRepository;
        }

        public async Task<bool> BuyArtworkAsync(Guid preOrderId)
        {
            try
            {
                if (!_helperService.IsTokenValid())
                {
                    throw new Exception(ServerErrorEnum.NOT_AUTHENTICATED);
                }
                var accLoggedId = await _accountRepository.GetAccountByIdAsync(_helperService.GetAccIdFromLogged()) ?? throw new Exception(ServerErrorEnum.NOT_AUTHENTICATED);
                var preOrder = await _preOrderRepository.GetPreOrderByIdAsync(preOrderId) ?? throw new Exception("PRE_ORDER_NOT_FOUND");
                if (preOrder.CustomerId != accLoggedId.Id)
                {
                    throw new Exception(ServerErrorEnum.NOT_AUTHORIZED);
                }
                var artwork = await _artworkRepository.GetArtworkByArtworkByIdAsync(preOrder.ArtworkId) ?? throw new Exception("ARTWORK_NOT_FOUND");
                if (artwork.Price > accLoggedId.Balance)
                {
                    throw new Exception("BALANCE_NOT_ENOUGH");
                }
                var status = await _statusRepository.GetStatusByNameAsync(OrderStatusEnum.PAID) ?? throw new Exception("STATUS_NOT_FOUND");
                // Check if artwork is sold
                if (artwork.OrderId != null)
                {
                    throw new Exception("ARTWORK_IS_SOLD");
                }
                Order order = new()
                {
                    AccountId = accLoggedId.Id,
                    StatusId = status.Id,
                    Payment = "ZaloPay"
                };

                var resultOrder = await _orderRepository.CreateOrderAsync(order);
                var creator = await _accountRepository.GetAccountByIdAsync(artwork.CreatorId) ?? throw new Exception("CREATOR_NOT_FOUND");
                if (resultOrder != null)
                {
                    accLoggedId.Balance -= artwork.Price ?? 0;
                    var resultAccount = await _accountRepository.UpdateAccountAsync(accLoggedId);
                    creator.Balance += artwork.Price ?? 0;
                    var resultCreator = await _accountRepository.UpdateAccountAsync(creator);
                    artwork.OrderId = resultOrder.Id;
                    var resultArtwork = await _artworkRepository.UpdateArtworkAsync(artwork);
                    if (resultAccount && resultArtwork)
                    {
                        // Delete pre-order
                        var resultPreOrder = await _preOrderRepository.DeletePreOrderAsync(preOrder);
                        if (resultPreOrder)
                        {
                            return true;
                        }
                        else
                        {
                            throw new Exception("ORDER_FAILED");
                        }
                    }
                    else
                    {
                        throw new Exception("ORDER_FAILED");
                    }
                }
                else
                {
                    throw new Exception("ORDER_FAILED");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<GetOrderDto>> GetAllOrder()
        {
            var order = await _orderRepository.GetListOrder();
            List<GetOrderDto> getOrderDto = order.Select(_ => new GetOrderDto
            {
                OrderId = _.Id,
                Payment = _.Payment,
                UserName = _.Account.FirstName + "" + _.Account.LastName,
                UserEmail = _.Account.Email,
                AccountId = _.AccountId,
                ListNameArtwork = _.Artworks?.Select(artworkName => artworkName.Name).ToList(),
                Artworks = _.Artworks?.Select(artwork => new GetArtworkDto
                {
                    ArtworkId = artwork.Id,
                    ArtworkName = artwork.Name,
                    StatusName = "PAID",
                    Image = artwork.Image,
                    Description = artwork.Description,
                    Price = artwork.Price
                }).ToList(),

            }).ToList();
            return getOrderDto;
        }

        public async Task<List<GetOrderDto>> GetListOrderByCustomerIdAsync(Guid accountId)
        {

            var order = await _orderRepository.GetListOrderByCustomerIdAsync(accountId);
            if (order == null)
            {
                throw new Exception($"Cant Not Find Order Have Accounr Id : {accountId} In Database");
            }
            List<GetOrderDto> getOrderDto = order.Select(_ => new GetOrderDto
            {
                OrderId = _.Id,
                Payment = _.Payment,
                UserName = _.Account.FirstName + "" + _.Account.LastName,
                UserEmail = _.Account.Email,
                AccountId = _.AccountId,
                ListNameArtwork = _.Artworks?.Select(artworkName => artworkName.Name).ToList(),
                Artworks = _.Artworks?.Select(artwork => new GetArtworkDto
                {
                    ArtworkId = artwork.Id,
                    ArtworkName = artwork.Name,
                    StatusName = "PAID",
                    Image = artwork.Image,
                    Description = artwork.Description,
                    Price = artwork.Price
                }).ToList(),

            }).ToList();
            return getOrderDto;
        }

        public async Task<GetOrderDto> GetOrderById(Guid orderId)
        {
            var order = await _orderRepository.GetOrderByIdAsync(orderId);
            if (order == null)
            {
                throw new Exception("Order Not Found");
            }
            GetOrderDto getOrderDto = new GetOrderDto
            {
                OrderId = order.Id,
                Payment = order.Payment,
                UserName = order.Account.FirstName + "" + order.Account.LastName,
                UserEmail = order.Account.Email,
                AccountId = order.AccountId,
                ListNameArtwork = order.Artworks?.Select(artworkName => artworkName.Name).ToList(),
                Artworks = order.Artworks?.Select(artwork => new GetArtworkDto
                {
                    ArtworkId = artwork.Id,
                    Image = artwork.Image,
                    ArtworkName = artwork.Name,
                    StatusName = "PAID",
                    Description = artwork.Description,
                    Price = artwork.Price
                }).ToList()
            };
            return getOrderDto;
        }
    }
}
