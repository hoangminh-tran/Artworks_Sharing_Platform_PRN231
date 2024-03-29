using Artworks_Sharing_Plaform_Api.Enum;
using Artworks_Sharing_Plaform_Api.Model;
using Artworks_Sharing_Plaform_Api.Model.Dto.ReqDto;
using Artworks_Sharing_Plaform_Api.Model.Dto.ResDto;
using Artworks_Sharing_Plaform_Api.Repository.Interface;
using Artworks_Sharing_Plaform_Api.Service.Interface;

namespace Artworks_Sharing_Plaform_Api.Service
{
    public class ArtworkService : IArtworkService
    {
        private readonly IArtworkRepository _artworkRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IStatusRepository _statusRepository;
        private readonly IHelpperService _helperService;
        private readonly IRoleRepository _roleRepository;
        private readonly ITypeOfArtworkRepository _typeOfArtworkRepository;
        private readonly IArtworkTypeRepository _artworkTypeRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly ILikeByRepository _likeByRepository;

        public ArtworkService(IArtworkRepository artworkRepository, IAccountRepository accountRepository, IStatusRepository statusRepository, IHelpperService helperService, IRoleRepository roleRepository, ITypeOfArtworkRepository typeOfArtworkRepository, IArtworkTypeRepository artworkTypeRepository, IOrderRepository orderRepository, ILikeByRepository likeByRepository)
        {
            _artworkRepository = artworkRepository;
            _accountRepository = accountRepository;
            _statusRepository = statusRepository;
            _helperService = helperService;
            _roleRepository = roleRepository;
            _typeOfArtworkRepository = typeOfArtworkRepository;
            _artworkTypeRepository = artworkTypeRepository;
            _orderRepository = orderRepository;
            _likeByRepository = likeByRepository;
        }

        public async Task<GetArtworkByCreatorResDto> GetArtworkByArtworkIdByCreatorAsync(Guid artworkId)
        {
            try
            {
                if (!_helperService.IsTokenValid())
                {
                    throw new Exception(ServerErrorEnum.NOT_AUTHENTICATED);
                }
                var artwork = await _artworkRepository.GetArtworkByIdAsync(artworkId) ?? throw new Exception("Artwork Not Found");
                var creator = await _accountRepository.GetAccountByIdAsync(artwork.CreatorId) ?? throw new Exception("Creator Not Found");
                var accuntLogged = await _accountRepository.GetAccountByIdAsync(_helperService.GetAccIdFromLogged()) ?? throw new Exception("Account Not Found");
                if (artwork.CreatorId != accuntLogged.Id)
                {
                    throw new Exception(ServerErrorEnum.NOT_AUTHORIZED);
                }
                var status = await _statusRepository.GetStatusByStatusIDAsync(artwork.StatusId) ?? throw new Exception("Status Not Found");
                var typeOfArtworkob = await _artworkTypeRepository.GetAllArtworkTypeByArtworkIdAsync(artworkId);
                List<GetTypeOfArtworkResDto> typeOfArtworkResDtos = new();
                foreach (var type in typeOfArtworkob)
                {
                    var typeOfArtwork = await _typeOfArtworkRepository.GetTypeOfArtworkByIdAsync(type.TypeOfArtworkId) ?? throw new Exception("TypeOfArtwork Not Found");
                    var typeOfArtworkResDto = new GetTypeOfArtworkResDto
                    {
                        Id = typeOfArtwork.Id,
                        TypeDescription = typeOfArtwork.TypeDescription,
                        Type = typeOfArtwork.Type
                    };
                    typeOfArtworkResDtos.Add(typeOfArtworkResDto);
                }
                var Status = await _statusRepository.GetStatusByStatusIDAsync(artwork.StatusId) ?? new Status();
                var order = await _orderRepository.GetOrderByIdAsync(artwork.OrderId ?? new Guid()) ?? new Order();
                var userOwner = await _accountRepository.GetAccountByIdAsync(order.AccountId) ?? new Account();
                GetArtworkByCreatorResDto get = new()
                {
                    ArtworkId = artwork.Id,
                    Title = artwork.Name,
                    Description = artwork.Description,
                    CreateDateTime = artwork.CreateDateTime,
                    Image = artwork.Image,
                    Price = artwork.Price ?? 0,
                    StatusName = status.StatusName,
                    TypeOfArtworks = typeOfArtworkResDtos,
                    UserOwnerName = userOwner.FirstName + userOwner.LastName
                };
                return get;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<GetArtworkByCustomerResDto> GetArtworkByArtworkIdByCustomerAsync(Guid artworkId)
        {
            try
            {
                if (!_helperService.IsTokenValid())
                {
                    throw new Exception(ServerErrorEnum.NOT_AUTHENTICATED);
                }
                var account = await _accountRepository.GetAccountByIdAsync(_helperService.GetAccIdFromLogged()) ?? throw new Exception("Account Not Found");
                var artwork = await _artworkRepository.GetArtworkByIdAsync(artworkId) ?? throw new Exception("Artwork Not Found");
                var order = await _orderRepository.GetOrderByIdAsync(artwork.OrderId ?? new Guid()) ?? throw new Exception("Order Not Found");
                if (order.AccountId != account.Id)
                {
                    throw new Exception(ServerErrorEnum.NOT_AUTHORIZED);
                }
                if (order.StatusId != (await _statusRepository.GetStatusByNameAsync(OrderStatusEnum.PAID) ?? throw new Exception("Server Error")).Id)
                {
                    throw new Exception("Order Not Paid");
                }
                var creator = await _accountRepository.GetAccountByIdAsync(artwork.CreatorId) ?? throw new Exception("Creator Not Found");
                var status = await _statusRepository.GetStatusByStatusIDAsync(artwork.StatusId) ?? throw new Exception("Status Not Found");
                var typeOfArtworkob = await _artworkTypeRepository.GetAllArtworkTypeByArtworkIdAsync(artworkId);
                List<GetTypeOfArtworkResDto> typeOfArtworkResDtos = new();
                foreach (var type in typeOfArtworkob)
                {
                    var typeOfArtwork = await _typeOfArtworkRepository.GetTypeOfArtworkByIdAsync(type.TypeOfArtworkId) ?? throw new Exception("TypeOfArtwork Not Found");
                    var typeOfArtworkResDto = new GetTypeOfArtworkResDto
                    {
                        Id = typeOfArtwork.Id,
                        TypeDescription = typeOfArtwork.TypeDescription,
                        Type = typeOfArtwork.Type
                    };
                    typeOfArtworkResDtos.Add(typeOfArtworkResDto);
                }
                GetArtworkByCustomerResDto get = new()
                {
                    ArtworkId = artwork.Id,
                    ArtworkName = artwork.Name,
                    Description = artwork.Description,
                    CreateDateTime = artwork.CreateDateTime,
                    Image = artwork.Image,
                    Price = artwork.Price ?? 0,
                    StatusName = status.StatusName,
                    ArtworkList = typeOfArtworkResDtos,
                    CreatorName = creator.FirstName + creator.LastName
                };
                return get;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<GetArtworkByGuest> GetArtworkByArtworkIdByGuestAsync(Guid artworkId)
        {
            try
            {
                var artwork = await _artworkRepository.GetArtworkByIdAsync(artworkId) ?? throw new Exception("Artwork Not Found");
                var creator = await _accountRepository.GetAccountByIdAsync(artwork.CreatorId) ?? throw new Exception("Creator Not Found");
                var status = await _statusRepository.GetStatusByStatusIDAsync(artwork.StatusId) ?? throw new Exception("Status Not Found");
                if (status.StatusName == ArtworkStatusEnum.PRIVATE)
                {
                    throw new Exception("Artwork Not Public");
                }
                var typeOfArtworkob = await _artworkTypeRepository.GetAllArtworkTypeByArtworkIdAsync(artworkId);
                List<GetTypeOfArtworkResDto> typeOfArtworkResDtos = new();
                foreach (var type in typeOfArtworkob)
                {
                    var typeOfArtwork = await _typeOfArtworkRepository.GetTypeOfArtworkByIdAsync(type.TypeOfArtworkId) ?? new Model.TypeOfArtwork();
                    var typeOfArtworkResDto = new GetTypeOfArtworkResDto
                    {
                        Id = typeOfArtwork.Id,
                        TypeDescription = typeOfArtwork.TypeDescription,
                        Type = typeOfArtwork.Type
                    };
                    typeOfArtworkResDtos.Add(typeOfArtworkResDto);
                }

                var listLikeArtwork = await _likeByRepository.GetListLikeByArtworkIdAsync(artworkId);
                var accuntLogged = await _accountRepository.GetAccountByIdAsync(_helperService.GetAccIdFromLoogedNotThrow());
                bool isLike = false;
                if (accuntLogged != null)
                {
                    if (listLikeArtwork.Exists(x => x.AccountId == accuntLogged.Id))
                    {
                        isLike = true;
                    }
                }

                GetArtworkByGuest get = new()
                {
                    ArtworkName = artwork.Name,
                    ArtworkTypeList = typeOfArtworkResDtos,
                    Description = artwork.Description,
                    CreatorName = creator.FirstName + " " + creator.LastName,
                    Image = artwork.Image,
                    IsSold = artwork.OrderId.HasValue ? true : false,
                    Price = artwork.Price ?? 0,
                    CreateDateTime = artwork.CreateDateTime,
                    LikeCount = listLikeArtwork.Count,
                    IsLike = isLike,
                    creatorId = artwork.CreatorId
                };
                return get;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<GetPublicArtworkResDto>> GetListArtworkAsync()
        {
            try
            {
                var listArtwork = await _artworkRepository.GetListArtworkAsync();
                var status = await _statusRepository.GetStatusByNameAsync("PUBLIC") ?? throw new Exception("Status Not Found");

                listArtwork = listArtwork.Where(p => p.StatusId == status.Id).ToList();

                List<GetPublicArtworkResDto> publicArtworks = new();
                foreach (var artwork in listArtwork)
                {
                    publicArtworks.Add(new GetPublicArtworkResDto
                    {
                        ArtworkId = artwork.Id,
                        Image = artwork.Image
                    });
                }
                return publicArtworks;
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<GetArtworkByCreatorResDto>> GetListArtworkByCreatorAsync()
        {
            try
            {
                if (!_helperService.IsTokenValid())
                {
                    throw new Exception(ServerErrorEnum.NOT_AUTHENTICATED);
                }
                var accLoggedId = await _accountRepository.GetAccountByIdAsync(_helperService.GetAccIdFromLogged()) ?? throw new Exception(ServerErrorEnum.NOT_AUTHENTICATED);
                var listArtwork = await _artworkRepository.GetListArtworkByCreatorIdAsync(accLoggedId.Id);
                List<GetArtworkByCreatorResDto> listArtworkResDto = new();
                foreach (var artwork in listArtwork)
                {
                    var status = await _statusRepository.GetStatusByStatusIDAsync(artwork.StatusId) ?? throw new Exception("Status Not Found");
                    var typeOfArtworkob = await _artworkTypeRepository.GetAllArtworkTypeByArtworkIdAsync(artwork.Id);
                    List<GetTypeOfArtworkResDto> typeOfArtworkResDtos = new();
                    foreach (var type in typeOfArtworkob)
                    {
                        var typeOfArtwork = await _typeOfArtworkRepository.GetTypeOfArtworkByIdAsync(type.TypeOfArtworkId) ?? throw new Exception("TypeOfArtwork Not Found");
                        var typeOfArtworkResDto = new GetTypeOfArtworkResDto
                        {
                            Id = typeOfArtwork.Id,
                            TypeDescription = typeOfArtwork.TypeDescription,
                            Type = typeOfArtwork.Type
                        };
                        typeOfArtworkResDtos.Add(typeOfArtworkResDto);
                    }
                    GetArtworkByCreatorResDto get = new()
                    {
                        ArtworkId = artwork.Id,
                        Title = artwork.Name,
                        Description = artwork.Description,
                        CreateDateTime = artwork.CreateDateTime,
                        Image = artwork.Image,
                        Price = artwork.Price ?? 0,
                        StatusName = status.StatusName,
                        TypeOfArtworks = typeOfArtworkResDtos
                    };
                    listArtworkResDto.Add(get);
                }
                return listArtworkResDto;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<GetArtworkByCustomerResDto>> GetListArtworkByCustomerAsync()
        {
            try
            {
                if (!_helperService.IsTokenValid())
                {
                    throw new Exception(ServerErrorEnum.NOT_AUTHENTICATED);
                }
                var accLoggedId = await _accountRepository.GetAccountByIdAsync(_helperService.GetAccIdFromLogged()) ?? throw new Exception(ServerErrorEnum.NOT_AUTHENTICATED);
                var listOrder = await _orderRepository.GetListOrderByCustomerIdAsync(accLoggedId.Id);
                // filler listOrder have status is PAID
                var statusPaid = await _statusRepository.GetStatusByNameAsync(OrderStatusEnum.PAID) ?? throw new Exception("Server Error");
                listOrder = listOrder.FindAll(x => x.StatusId == statusPaid.Id);
                List<GetArtworkByCustomerResDto> listArtworkResDto = new();
                foreach (var order in listOrder)
                {
                    if (order.StatusId == (await _statusRepository.GetStatusByNameAsync(OrderStatusEnum.PAID) ?? throw new Exception("Server Error")).Id)
                    {
                        var artwork = await _artworkRepository.GetArtworkByOrderIdAsync(order.Id) ?? throw new Exception("Artwork Not Found");
                        var creator = await _accountRepository.GetAccountByIdAsync(artwork.CreatorId) ?? throw new Exception("Creator Not Found");
                        var status = await _statusRepository.GetStatusByStatusIDAsync(artwork.StatusId) ?? throw new Exception("Status Not Found");
                        var typeOfArtworkob = await _artworkTypeRepository.GetAllArtworkTypeByArtworkIdAsync(artwork.Id);
                        List<GetTypeOfArtworkResDto> typeOfArtworkResDtos = new();
                        foreach (var type in typeOfArtworkob)
                        {
                            var typeOfArtwork = await _typeOfArtworkRepository.GetTypeOfArtworkByIdAsync(type.TypeOfArtworkId) ?? throw new Exception("TypeOfArtwork Not Found");
                            var typeOfArtworkResDto = new GetTypeOfArtworkResDto
                            {
                                Id = typeOfArtwork.Id,
                                TypeDescription = typeOfArtwork.TypeDescription,
                                Type = typeOfArtwork.Type
                            };
                            typeOfArtworkResDtos.Add(typeOfArtworkResDto);
                        }
                        GetArtworkByCustomerResDto get = new()
                        {
                            ArtworkId = artwork.Id,
                            ArtworkName = artwork.Name,
                            Description = artwork.Description,
                            CreateDateTime = artwork.CreateDateTime,
                            Image = artwork.Image,
                            Price = artwork.Price ?? 0,
                            StatusName = status.StatusName,
                            ArtworkList = typeOfArtworkResDtos,
                            CreatorName = creator.FirstName + creator.LastName
                        };
                        listArtworkResDto.Add(get);
                    }
                }
                return listArtworkResDto;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<GetArtworkResDto>> GetListArtworkOwnByUserAsync()
        {
            try
            {
                if (!_helperService.IsTokenValid())
                {
                    throw new Exception(ServerErrorEnum.NOT_AUTHENTICATED);
                }
                var accLoggedId = await _accountRepository.GetAccountByIdAsync(_helperService.GetAccIdFromLogged()) ?? throw new Exception(ServerErrorEnum.NOT_AUTHENTICATED);
                var listArtwork = await _artworkRepository.GetListArtworkOwnByUserAsync(accLoggedId.Id);
                List<GetArtworkResDto> listArtworkResDto = new();
                foreach (var artwork in listArtwork)
                {
                    var status = await _statusRepository.GetStatusByStatusIDAsync(artwork.StatusId) ?? throw new Exception("Status Not Found");
                    var typeOfArtworkob = await _artworkTypeRepository.GetAllArtworkTypeByArtworkIdAsync(artwork.Id);
                    List<GetTypeOfArtworkResDto> typeOfArtworkResDtos = new();
                    foreach (var type in typeOfArtworkob)
                    {
                        var typeOfArtwork = await _typeOfArtworkRepository.GetTypeOfArtworkByIdAsync(type.TypeOfArtworkId) ?? throw new Exception("TypeOfArtwork Not Found");
                        var typeOfArtworkResDto = new GetTypeOfArtworkResDto
                        {
                            Id = typeOfArtwork.Id,
                            TypeDescription = typeOfArtwork.TypeDescription,
                            Type = typeOfArtwork.Type
                        };
                        typeOfArtworkResDtos.Add(typeOfArtworkResDto);
                    }
                    var creator = await _accountRepository.GetAccountByIdAsync(artwork.CreatorId) ?? throw new Exception("Creator Not Found");
                    GetArtworkResDto get = new()
                    {
                        ArtworkId = artwork.Id,
                        ArtworkName = artwork.Name,
                        ArtworkDescription = artwork.Description,
                        CreateDateTime = artwork.CreateDateTime,
                        CreatorName = creator.FirstName + " " + creator.LastName,
                        Image = artwork.Image,
                        TypeOfArtwork = typeOfArtworkResDtos,
                        Price = artwork.Price ?? 0,
                    };
                    listArtworkResDto.Add(get);
                }
                return listArtworkResDto;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> UpdateArtwork(UpdateArtworkDto artworkRequest)
        {
            try
            {
                if (!_helperService.IsTokenValid())
                {
                    throw new Exception(ServerErrorEnum.NOT_AUTHENTICATED);
                }
                var accLoggedId = await _accountRepository.GetAccountByIdAsync(_helperService.GetAccIdFromLogged()) ?? throw new Exception(ServerErrorEnum.NOT_AUTHENTICATED);
                var role = await _roleRepository.GetRoleByNameAsync(RoleEnum.CREATOR) ?? throw new Exception(ServerErrorEnum.SERVER_ERROR);
                if (accLoggedId.RoleId != role.Id)
                {
                    throw new Exception(ServerErrorEnum.NOT_AUTHORIZED);
                }
                Status? status;
                status = await _statusRepository.GetStatusByNameAsync(artworkRequest.Status.Trim().ToUpper());

                if (status == null)
                {
                    throw new Exception(ServerErrorEnum.SERVER_ERROR);
                }

                Artwork oldArtwork = await _artworkRepository.GetArtworkByArtworkByIdAsync(artworkRequest.ArtworkId);

                if(artworkRequest.Price <= 0)
                {
                    throw new Exception("Price must be positive!");
                }

                oldArtwork.Name = artworkRequest.Name;
                oldArtwork.Description = artworkRequest.Description;
                oldArtwork.Price = artworkRequest.Price;
                oldArtwork.StatusId = status.Id;
                var newArtwork = await _artworkRepository.UpdateArtworkAsync(oldArtwork);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> UploadArtworkByCreatorAsync(IFormFile file, UploadArtworkReqDto imgDes)
        {
            try
            {
                if (!_helperService.IsTokenValid())
                {
                    throw new Exception(ServerErrorEnum.NOT_AUTHENTICATED);
                }
                var accLoggedId = await _accountRepository.GetAccountByIdAsync(_helperService.GetAccIdFromLogged()) ?? throw new Exception(ServerErrorEnum.NOT_AUTHENTICATED);

                Status? status;

                if (imgDes.IsPublic)
                {
                    status = await _statusRepository.GetStatusByNameAsync(ArtworkStatusEnum.PUBLIC);
                }
                else
                {
                    status = await _statusRepository.GetStatusByNameAsync(ArtworkStatusEnum.PRIVATE);
                }

                if (status == null)
                {
                    throw new Exception(ServerErrorEnum.SERVER_ERROR);
                }

                // Check input TypeOfArtwork data
                foreach (var type in imgDes.TypeOfArtwork)
                {
                    if (await _typeOfArtworkRepository.GetTypeOfArtworkByIdAsync(type) == null)
                    {
                        throw new Exception("Id TypeOfArtwork Not Found");
                    }
                }

                // Change file image to byte array
                byte[]? imageBytes = null;
                using (var ms = new MemoryStream())
                {
                    file.CopyTo(ms);
                    imageBytes = ms.ToArray();
                }
                // Create artwork object
                Artwork artwork = new()
                {
                    CreatorId = accLoggedId.Id,
                    Name = imgDes.ArtworkName,
                    Description = imgDes.ArtworkDescription,
                    Price = imgDes.ArtworkPrice,
                    Image = imageBytes,
                    StatusId = status.Id
                };
                var newArtwork = await _artworkRepository.CreateArtworkAsync(artwork);

                // Create artwork type object
                foreach (var type in imgDes.TypeOfArtwork)
                {
                    ArtworkType artworkType = new()
                    {
                        ArtworkId = newArtwork.Id,
                        TypeOfArtworkId = type
                    };
                    await _artworkTypeRepository.CreateArtworkTypeAsync(artworkType);
                }
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }       
        
        public async Task<List<GetArtworkResDto>> GetListArtworkByTypeOfArtworkIdAsync(Guid typeOfArtworkID)
        {
            try
            {
                var statusName = await _statusRepository.GetStatusByNameAsync(StatusEnum.ACTIVE) ?? throw new Exception(ServerErrorEnum.SERVER_ERROR);     

                var typeOfArtworkExist = await _typeOfArtworkRepository.GetTypeOfArtworkByIdAndStatusAsync(typeOfArtworkID, statusName.Id);

                if(typeOfArtworkExist == null)
                {
                    throw new Exception(TypeOfArtworkErrorEnum.TYPE_OF_ARTWORK_NOT_FOUND);
                }

                var artworkType = await _artworkTypeRepository.GetArtworkTypeAsyncByTypeOfArtworkId(typeOfArtworkID);
                List<GetArtworkResDto> listArtworkResDto = new ();
                List<Artwork> listArtwork = new ();
                foreach(var at in artworkType)
                {
                    listArtwork.Add(
                        await _artworkRepository.GetArtworkByIdAsync(at.ArtworkId)
                    );
                }
                if(listArtwork.Count == 0)
                {
                    return listArtworkResDto;
                }
                foreach (var artwork in listArtwork)
                {
                    var status = await _statusRepository.GetStatusByStatusIDAsync(artwork.StatusId) ?? throw new Exception("Status Not Found");
                    var typeOfArtworkob = await _artworkTypeRepository.GetAllArtworkTypeByArtworkIdAsync(artwork.Id);
                    List<GetTypeOfArtworkResDto> typeOfArtworkResDtos = new();
                    foreach (var type in typeOfArtworkob)
                    {
                        var typeOfArtwork = await _typeOfArtworkRepository.GetTypeOfArtworkByIdAsync(type.TypeOfArtworkId) ?? throw new Exception("TypeOfArtwork Not Found");
                        var typeOfArtworkResDto = new GetTypeOfArtworkResDto
                        {
                            Id = typeOfArtwork.Id,
                            TypeDescription = typeOfArtwork.TypeDescription,
                            Type = typeOfArtwork.Type
                        };
                        typeOfArtworkResDtos.Add(typeOfArtworkResDto);
                    }
                    var creator = await _accountRepository.GetAccountByIdAsync(artwork.CreatorId) ?? throw new Exception("Creator Not Found");
                    GetArtworkResDto get = new()
                    {
                        ArtworkId = artwork.Id,
                        Image = artwork.Image,
                    };
                    listArtworkResDto.Add(get);
                }
                return listArtworkResDto;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<GetArtworkResDto>> GetListArtworkByCreatorIdAsync(Guid creatorId)
        {
            try
            {
                List<GetArtworkResDto> listArtworkResDto = new();
                var statusArtwork = await _statusRepository.GetStatusByNameAsync(ArtworkStatusEnum.PUBLIC) ?? throw new Exception("Status Not Found");
                var listArtwork = await _artworkRepository.GetListArtworkByCreatorIdAndStatusAsync(creatorId, statusArtwork.Id);              
                foreach (var artwork in listArtwork)
                {                                  
                    GetArtworkResDto get = new()
                    {
                        ArtworkId = artwork.Id,                       
                        Image = artwork.Image,                   
                    };
                    listArtworkResDto.Add(get);
                }
                return listArtworkResDto;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<GetArtworkResDto>> GetListArtworkByArtworkNameAsync(string? artworkName)
        {
            try
            {       
                var statusName = await _statusRepository.GetStatusByNameAsync(ArtworkStatusEnum.PUBLIC) ?? throw new Exception(ServerErrorEnum.SERVER_ERROR);                
                List<GetArtworkResDto> listArtworkResDto = new();
                var listArtwork = await _artworkRepository.GetListArtworkByArtworkNameAsync(artworkName, statusName.Id);
                if (listArtwork.Count == 0)
                {
                    return listArtworkResDto;
                }
                foreach (var artwork in listArtwork)
                {
                    var status = await _statusRepository.GetStatusByStatusIDAsync(artwork.StatusId) ?? throw new Exception("Status Not Found");           
                    GetArtworkResDto get = new()
                    {
                        ArtworkId = artwork.Id,
                        Image = artwork.Image,
                    };
                    listArtworkResDto.Add(get);
                }
                return listArtworkResDto;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<GetArtworkResDto>> GetListArtworkByFilterListTypeOfArtworkAndArtistAsync(FilterArtworkByListTypeAndArtistReqDto? dto)
        {
            try
            {
                if (dto != null)
                {
                    if (dto.TypeOfArtworkIds != null)
                    {
                        foreach (var type in dto.TypeOfArtworkIds)
                        {
                            if (await _typeOfArtworkRepository.GetTypeOfArtworkByIdAsync(type) == null)
                            {
                                throw new Exception("Id TypeOfArtwork Not Found");
                            }
                        }
                    }
                    if (dto.ArtistId != null)
                    {
                        if (await _accountRepository.GetAccountByIdAsync(dto.ArtistId ?? new Guid()) == null)
                        {
                            throw new Exception("Id Artist Not Found");
                        }
                    }
                }                         

                var statusName = await _statusRepository.GetStatusByNameAsync(ArtworkStatusEnum.PUBLIC) ?? throw new Exception(ServerErrorEnum.SERVER_ERROR);
                if (dto == null)
                {
                    var listArtwork1 = await _artworkRepository.GetListArtworkByFilterListTypeOfArtworkAndArtistAsync(null, null, statusName.Id);
                    List<GetArtworkResDto> listArtworkResDto1 = new();
                    foreach (var artwork in listArtwork1)
                    {
                        GetArtworkResDto get = new()
                        {
                            ArtworkId = artwork.Id,
                            Image = artwork.Image,
                        };
                        listArtworkResDto1.Add(get);
                    }
                    return listArtworkResDto1;
                }

                var listArtwork = await _artworkRepository.GetListArtworkByFilterListTypeOfArtworkAndArtistAsync(dto.TypeOfArtworkIds, dto.ArtistId, statusName.Id);
                List<GetArtworkResDto> listArtworkResDto = new();
                foreach( var artwork in listArtwork)
                {
                    GetArtworkResDto get = new()
                    {
                        ArtworkId = artwork.Id,
                        Image = artwork.Image,
                    };
                    listArtworkResDto.Add(get);
                }
                return listArtworkResDto;
            } catch (Exception)
            {
                throw;
            }
        }
    }
}
