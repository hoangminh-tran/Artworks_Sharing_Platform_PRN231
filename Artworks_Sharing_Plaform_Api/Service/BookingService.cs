using Artworks_Sharing_Plaform_Api.Enum;
using Artworks_Sharing_Plaform_Api.Model;
using Artworks_Sharing_Plaform_Api.Model.Dto.ReqDto;
using Artworks_Sharing_Plaform_Api.Model.Dto.ResDto;
using Artworks_Sharing_Plaform_Api.Repository.Interface;
using Artworks_Sharing_Plaform_Api.Service.Interface;
using System.Diagnostics;

namespace Artworks_Sharing_Plaform_Api.Service
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IBookingArtworkTypeRepository _bookingArtworkTypeRepository;
        private readonly ITypeOfArtworkRepository _typeOfArtworkRepository;
        private readonly IHelpperService _helperService;
        private readonly IAccountRepository _accountRepository;
        private readonly IBookingArtworkRepository _bookingArtworkRepository;
        private readonly IArtworkRepository _artworkRepository;
        private readonly IArtworkTypeRepository _artworkTypeRepository;
        private readonly IStatusRepository _statusRepository;
        private readonly IRequestArtworkRepository _requestArtworkRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IOrderRepository _orderRepository;

        public BookingService(IBookingRepository bookingRepository, IBookingArtworkTypeRepository bookingArtworkTypeRepository, ITypeOfArtworkRepository typeOfArtworkRepository, IHelpperService helperService, IAccountRepository accountRepository, IBookingArtworkRepository bookingArtworkRepository, IArtworkRepository artworkRepository, IArtworkTypeRepository artworkTypeRepository, IStatusRepository statusRepository, IRequestArtworkRepository requestArtworkRepository, IRoleRepository roleRepository, IOrderRepository orderRepository)
        {
            _bookingRepository = bookingRepository;
            _bookingArtworkTypeRepository = bookingArtworkTypeRepository;
            _typeOfArtworkRepository = typeOfArtworkRepository;
            _helperService = helperService;
            _accountRepository = accountRepository;
            _bookingArtworkRepository = bookingArtworkRepository;
            _artworkRepository = artworkRepository;
            _artworkTypeRepository = artworkTypeRepository;
            _statusRepository = statusRepository;
            _requestArtworkRepository = requestArtworkRepository;
            _roleRepository = roleRepository;
            _orderRepository = orderRepository;
        }

        public async Task<bool> CreateBookingAsync(CreateBookingArtworkReqDto bookingArtworkReqDto)
        {
            try
            {
                if (!_helperService.IsTokenValid())
                {
                    throw new Exception("Invalid Token");
                }
                var accLoggedId = await _accountRepository.GetAccountByIdAsync(_helperService.GetAccIdFromLogged()) ?? throw new Exception("Invalid Token");
                foreach (var item in bookingArtworkReqDto.ListTypeOfArtwork)
                {
                    var typeOfArtwork = await _typeOfArtworkRepository.GetTypeOfArtworkByIdAsync(item);
                    if (typeOfArtwork == null)
                    {
                        throw new Exception("Type of Artwork not found");
                    }
                }
                var creator = await _accountRepository.GetAccountByIdAsync(bookingArtworkReqDto.CreatorId) ?? throw new Exception("Creator not found");
                var statiusWaiting = await _statusRepository.GetStatusByNameAsync(BookingStatusEnum.PENDING) ?? throw new Exception("Status not found");
                Booking booking = new()
                {
                    UserId = accLoggedId.Id,
                    CreatorId = creator.Id,
                    Description = bookingArtworkReqDto.ContentBooking,
                    Price = bookingArtworkReqDto.Price,
                    StatusId = statiusWaiting.Id
                };
                var bookingCreated = await _bookingRepository.CreateBookingAsync(booking) ?? throw new Exception(ServerErrorEnum.SERVER_ERROR);
                foreach (var item in bookingArtworkReqDto.ListTypeOfArtwork)
                {
                    BookingArtworkType bookingArtworkType = new()
                    {
                        BookingId = bookingCreated.Id,
                        TypeOfArtworkId = item
                    };
                    await _bookingArtworkTypeRepository.CreateBookingArtworkTypeAsync(bookingArtworkType);
                }
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> UpdateStatusBookingAsync(UpdateStatusBookingResDto updateStatusBookingResDto)
        {
            try
            {
                if (!_helperService.IsTokenValid())
                {
                    throw new Exception("Invalid Token");
                }
                var accLoggedId = await _accountRepository.GetAccountByIdAsync(_helperService.GetAccIdFromLogged()) ?? throw new Exception("Invalid Token");
                var booking = await _bookingRepository.GetBookingByIdAsync(updateStatusBookingResDto.BookingId) ?? throw new Exception("Booking not found");
                if (booking.CreatorId != accLoggedId.Id)
                {
                    throw new Exception("Not authorized");
                }
                var status = await _statusRepository.GetStatusByStatusIDAsync(updateStatusBookingResDto.StatusId) ?? throw new Exception("Status not found");
                booking.StatusId = status.Id;
                return await _bookingRepository.UpdateBookingAsync(booking);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> UploadArtworkByBookingIdAsync(IFormFile file, CreateUploadArtworkForBookingReqDto createUploadArtworkForBookingReqDto)
        {
            try
            {
                if (!_helperService.IsTokenValid())
                {
                    throw new Exception("Invalid Token");
                }
                var accLoggedId = await _accountRepository.GetAccountByIdAsync(_helperService.GetAccIdFromLogged()) ?? throw new Exception("Invalid Token");
                var booking = await _bookingRepository.GetBookingByIdAsync(createUploadArtworkForBookingReqDto.BookingId) ?? throw new Exception("Booking not found");
                if (booking.CreatorId != accLoggedId.Id)
                {
                    throw new Exception("Not authorized");
                }
                byte[]? imageBytes = null;
                if (file != null)
                {
                    using (var ms = new MemoryStream())
                    {
                        file.CopyTo(ms);
                        imageBytes = ms.ToArray();
                    }
                }
                if (imageBytes == null)
                {
                    throw new Exception("Image not found");
                }
                var statusArtwork = await _statusRepository.GetStatusByNameAsync(ArtworkStatusEnum.PUBLIC) ?? throw new Exception("Status not found");
                Artwork artwork = new()
                {
                    CreatorId = accLoggedId.Id,
                    Name = createUploadArtworkForBookingReqDto.Title,
                    Description = createUploadArtworkForBookingReqDto.Description,
                    Image = imageBytes,
                    StatusId = statusArtwork.Id
                };
                var artworkObj = await _artworkRepository.CreateArtworkAsync(artwork) ?? throw new Exception(ServerErrorEnum.SERVER_ERROR);
                var listBookingArtworkType = await _bookingArtworkTypeRepository.GetListBookingArtworkTypeByBookingIdAsync(createUploadArtworkForBookingReqDto.BookingId);
                foreach (var item in listBookingArtworkType)
                {
                    ArtworkType artworkType = new()
                    {
                        ArtworkId = artworkObj.Id,
                        TypeOfArtworkId = item.TypeOfArtworkId
                    };
                    await _artworkTypeRepository.CreateArtworkTypeAsync(artworkType);
                }
                BookingArtwork bookingArtwork = new()
                {
                    BookingId = booking.Id,
                    ArtworkId = artworkObj.Id
                };
                var bookingArtworkObj = await _bookingArtworkRepository.CreateBookingArworkAsync(bookingArtwork) ?? throw new Exception(ServerErrorEnum.SERVER_ERROR);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> UploadArtworkByRequestArtworkAsync(IFormFile file, CreateUploadArtworkForRequestBookingArtworkResDto resDto)
        {
            try
            {
                if (!_helperService.IsTokenValid())
                {
                    throw new Exception("Invalid Token");
                }
                var accLoggedId = await _accountRepository.GetAccountByIdAsync(_helperService.GetAccIdFromLogged()) ?? throw new Exception("Invalid Token");
                var requestArtwork = await _requestArtworkRepository.GetRequestArtworkByRequestArtworkId(resDto.RequestArtworkId) ?? throw new Exception("Request Artwork not found");
                var booking = await _bookingRepository.GetBookingByIdAsync(requestArtwork.BookingId) ?? throw new Exception("Booking not found");
                if (booking.UserId != accLoggedId.Id)
                {
                    throw new Exception("Not authorized");
                }
                byte[]? imageBytes = null;
                if (file != null)
                {
                    using (var ms = new MemoryStream())
                    {
                        file.CopyTo(ms);
                        imageBytes = ms.ToArray();
                    }
                }
                if (imageBytes == null)
                {
                    throw new Exception("Image not found");
                }
                Artwork artwork = new()
                {
                    CreatorId = accLoggedId.Id,
                    Name = resDto.Title,
                    Description = resDto.Description,
                    Image = imageBytes,
                };
                var artworkObj = await _artworkRepository.CreateArtworkAsync(artwork) ?? throw new Exception(ServerErrorEnum.SERVER_ERROR);
                var listBookingArtworkType = await _bookingArtworkTypeRepository.GetListBookingArtworkTypeByBookingIdAsync(requestArtwork.BookingId);
                foreach (var item in listBookingArtworkType)
                {
                    ArtworkType artworkType = new()
                    {
                        ArtworkId = artworkObj.Id,
                        TypeOfArtworkId = item.TypeOfArtworkId
                    };
                    await _artworkTypeRepository.CreateArtworkTypeAsync(artworkType);
                }
                BookingArtwork bookingArtwork = new()
                {
                    BookingId = booking.Id,
                    ArtworkId = artworkObj.Id
                };
                var bookingArtworkObj = await _bookingArtworkRepository.CreateBookingArworkAsync(bookingArtwork) ?? throw new Exception(ServerErrorEnum.SERVER_ERROR);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> CreateRequestBookingArtworkByBookingIdAsync(CreateRequestBookingArtworkResDto createRequestBookingArtworkResDto)
        {
            try
            {
                if (!_helperService.IsTokenValid())
                {
                    throw new Exception("Invalid Token");
                }
                var accLoggedId = await _accountRepository.GetAccountByIdAsync(_helperService.GetAccIdFromLogged()) ?? throw new Exception("Invalid Token");
                var booking = await _bookingRepository.GetBookingByIdAsync(createRequestBookingArtworkResDto.BookingId) ?? throw new Exception("Booking not found");
                if (booking.UserId != accLoggedId.Id)
                {
                    throw new Exception("Not authorized");
                }
                var status = await _statusRepository.GetStatusByNameAsync(RequestArtworkStatusEnum.PENDING) ?? throw new Exception("Status not found");
                RequestArtwork requestArtwork = new()
                {
                    BookingId = booking.Id,
                    Description = createRequestBookingArtworkResDto.ContentRequest,
                    StatusId = status.Id
                };
                return await _requestArtworkRepository.CreateRequestArtworkAsync(requestArtwork);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<GetBookingForCustomerResDto>> GetListBookingByCustomerIdAsync()
        {
            try
            {
                if (!_helperService.IsTokenValid())
                {
                    throw new Exception("Invalid Token");
                }
                var accLoggedId = await _accountRepository.GetAccountByIdAsync(_helperService.GetAccIdFromLogged()) ?? throw new Exception("Invalid Token");
                var listBooking = await _bookingRepository.GetListBookingByCustomerIdAsync(accLoggedId.Id);
                List<GetBookingForCustomerResDto> listBookingRes = new();
                foreach (var item in listBooking)
                {
                    var listTypeOfArtwork = await _bookingArtworkTypeRepository.GetListBookingArtworkTypeByBookingIdAsync(item.Id) ?? new List<BookingArtworkType>();
                    var listRequestArtwork = await _requestArtworkRepository.GetListRequestArtworkByBookingIdAsync(item.Id) ?? new List<RequestArtwork>();
                    List<GetTypeOfArtworkResDto> listTypeOfArtworkRes = new();
                    foreach (var item1 in listTypeOfArtwork)
                    {
                        var typeOfArtwork = await _typeOfArtworkRepository.GetTypeOfArtworkByIdAsync(item1.TypeOfArtworkId) ?? new TypeOfArtwork();
                        GetTypeOfArtworkResDto typeOfArtworkRes = new()
                        {
                            Id = typeOfArtwork.Id,
                            Type = typeOfArtwork.Type,
                            TypeDescription = typeOfArtwork.TypeDescription
                        };
                        listTypeOfArtworkRes.Add(typeOfArtworkRes);
                    }
                    List<GetRequestBookingResDto> listRequestBookingRes = new();
                    foreach (var item2 in listRequestArtwork)
                    {
                        var status = await _statusRepository.GetStatusByStatusIDAsync(item2.StatusId) ?? throw new Exception("Status not found");
                        var bookingArtworkByRequest = await _bookingArtworkRepository.GetBookingArtworkByRequestArtworkIdAsync(item2.Id) ?? new BookingArtwork();
                        var imageRequest = await _artworkRepository.GetArtworkByIdAsync(bookingArtworkByRequest.ArtworkId) ?? new Artwork();
                        GetRequestBookingResDto requestBookingRes = new()
                        {
                            Description = item2.Description ?? "",
                            StatusName = status.StatusName,
                            Image = imageRequest.Image,
                            CreateDateTime = item2.CreateDateTime
                        };
                        listRequestBookingRes.Add(requestBookingRes);
                    }
                    var bookingArtworkByBooking = await _bookingArtworkRepository.GetBookingArtworkByBookingIdAsync(item.Id) ?? new BookingArtwork();
                    var image = await _artworkRepository.GetArtworkByIdAsync(bookingArtworkByBooking.ArtworkId) ?? new Artwork();
                    var statusBooking = await _statusRepository.GetStatusByStatusIDAsync(item.StatusId) ?? throw new Exception("Status not found");
                    var creator = await _accountRepository.GetAccountByIdAsync(item.CreatorId) ?? throw new Exception("Creator not found");
                    GetBookingForCustomerResDto bookingRes = new()
                    {
                        BookingId = item.Id,
                        CreatorName = creator.FirstName + " " + creator.LastName,
                        ListTypeOfArtwork = listTypeOfArtworkRes,
                        StatusName = statusBooking.StatusName,
                        Description = item.Description,
                        Price = item.Price,
                        Image = image.Image,
                        RequestBooking = listRequestBookingRes,
                        CreateDateTime = item.CreateDateTime
                    };
                    listBookingRes.Add(bookingRes);
                }
                return listBookingRes;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<GetBookingForCustomerResDto> GetBookingByBookingIdByCustomerAsync(Guid bookingId)
        {
            try
            {
                if (!_helperService.IsTokenValid())
                {
                    throw new Exception("Invalid Token");
                }
                var accLoggedId = await _accountRepository.GetAccountByIdAsync(_helperService.GetAccIdFromLogged()) ?? throw new Exception("Invalid Token");
                var booking = await _bookingRepository.GetBookingByIdAsync(bookingId) ?? throw new Exception("Booking not found");
                if (booking.UserId != accLoggedId.Id)
                {
                    throw new Exception("Not authorized");
                }
                var listTypeOfArtwork = await _bookingArtworkTypeRepository.GetListBookingArtworkTypeByBookingIdAsync(booking.Id) ?? new List<BookingArtworkType>();
                var listRequestArtwork = await _requestArtworkRepository.GetListRequestArtworkByBookingIdAsync(booking.Id) ?? new List<RequestArtwork>();
                List<GetTypeOfArtworkResDto> listTypeOfArtworkRes = new();
                foreach (var item in listTypeOfArtwork)
                {
                    var typeOfArtwork = await _typeOfArtworkRepository.GetTypeOfArtworkByIdAsync(item.TypeOfArtworkId) ?? new TypeOfArtwork();
                    GetTypeOfArtworkResDto typeOfArtworkRes = new()
                    {
                        Id = typeOfArtwork.Id,
                        Type = typeOfArtwork.Type,
                        TypeDescription = typeOfArtwork.TypeDescription
                    };
                    listTypeOfArtworkRes.Add(typeOfArtworkRes);
                }
                List<GetRequestBookingResDto> listRequestBookingRes = new();
                foreach (var item in listRequestArtwork)
                {
                    var status = await _statusRepository.GetStatusByStatusIDAsync(item.StatusId) ?? throw new Exception("Status not found");
                    var bookingArtworkByRequest = await _bookingArtworkRepository.GetBookingArtworkByRequestArtworkIdAsync(item.Id) ?? new BookingArtwork();
                    var imageRequest = await _artworkRepository.GetArtworkByIdAsync(bookingArtworkByRequest.ArtworkId) ?? new Artwork();
                    GetRequestBookingResDto requestBookingRes = new()
                    {
                        RequestBookingId = item.Id,
                        Description = item.Description ?? "",
                        StatusName = status.StatusName,
                        Image = imageRequest.Image,
                        CreateDateTime = item.CreateDateTime
                    };
                    listRequestBookingRes.Add(requestBookingRes);
                }
                var bookingArtworkByBooking = await _bookingArtworkRepository.GetBookingArtworkByBookingIdAsync(booking.Id) ?? new BookingArtwork();
                var image = await _artworkRepository.GetArtworkByIdAsync(bookingArtworkByBooking.ArtworkId) ?? new Artwork();
                var statusBooking = await _statusRepository.GetStatusByStatusIDAsync(booking.StatusId) ?? throw new Exception("Status not found");
                var creator = await _accountRepository.GetAccountByIdAsync(booking.CreatorId) ?? throw new Exception("Creator not found");
                GetBookingForCustomerResDto bookingRes = new()
                {
                    BookingId = booking.Id,
                    CreatorName = creator.FirstName + " " + creator.LastName,
                    ListTypeOfArtwork = listTypeOfArtworkRes,
                    StatusName = statusBooking.StatusName,
                    Description = booking.Description,
                    Price = booking.Price,
                    Image = image.Image,
                    RequestBooking = listRequestBookingRes,
                    CreateDateTime = booking.CreateDateTime
                };
                return bookingRes;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<GetBookingForCreatorResDto>> GetListBookingByCreatorIdAsync()
        {
            try
            {
                if (!_helperService.IsTokenValid())
                {
                    throw new Exception("Invalid Token");
                }
                var accLoggedId = await _accountRepository.GetAccountByIdAsync(_helperService.GetAccIdFromLogged()) ?? throw new Exception("Invalid Token");
                var listBooking = await _bookingRepository.GetListBookingByCreatorIdAsync(accLoggedId.Id);
                List<GetBookingForCreatorResDto> listBookingRes = new();
                foreach (var item in listBooking)
                {
                    var listTypeOfArtwork = await _bookingArtworkTypeRepository.GetListBookingArtworkTypeByBookingIdAsync(item.Id) ?? new List<BookingArtworkType>();
                    var listRequestArtwork = await _requestArtworkRepository.GetListRequestArtworkByBookingIdAsync(item.Id) ?? new List<RequestArtwork>();
                    List<GetTypeOfArtworkResDto> listTypeOfArtworkRes = new();
                    foreach (var item1 in listTypeOfArtwork)
                    {
                        var typeOfArtwork = await _typeOfArtworkRepository.GetTypeOfArtworkByIdAsync(item1.TypeOfArtworkId) ?? new TypeOfArtwork();
                        GetTypeOfArtworkResDto typeOfArtworkRes = new()
                        {
                            Id = typeOfArtwork.Id,
                            Type = typeOfArtwork.Type,
                            TypeDescription = typeOfArtwork.TypeDescription
                        };
                        listTypeOfArtworkRes.Add(typeOfArtworkRes);
                    }
                    List<GetRequestBookingResDto> listRequestBookingRes = new();
                    foreach (var item2 in listRequestArtwork)
                    {
                        var status = await _statusRepository.GetStatusByStatusIDAsync(item2.StatusId) ?? throw new Exception("Status not found");
                        var bookingArtworkByRequest = await _bookingArtworkRepository.GetBookingArtworkByRequestArtworkIdAsync(item2.Id) ?? new BookingArtwork();
                        var imageRequest = await _artworkRepository.GetArtworkByIdAsync(bookingArtworkByRequest.ArtworkId) ?? new Artwork();
                        GetRequestBookingResDto requestBookingRes = new()
                        {
                            RequestBookingId = item2.Id,
                            Description = item2.Description ?? "",
                            StatusName = status.StatusName,
                            Image = imageRequest.Image,
                            CreateDateTime = item2.CreateDateTime
                        };
                        listRequestBookingRes.Add(requestBookingRes);
                    }
                    var bookingArtworkByBooking = await _bookingArtworkRepository.GetBookingArtworkByBookingIdAsync(item.Id) ?? new BookingArtwork();
                    var image = await _artworkRepository.GetArtworkByIdAsync(bookingArtworkByBooking.ArtworkId) ?? new Artwork();
                    var statusBooking = await _statusRepository.GetStatusByStatusIDAsync(item.StatusId) ?? throw new Exception("Status not found");
                    var user = await _accountRepository.GetAccountByIdAsync(item.UserId) ?? throw new Exception("User not found");
                    GetBookingForCreatorResDto bookingRes = new()
                    {
                        BookingId = item.Id,
                        UserName = user.FirstName + " " + user.LastName,
                        ListTypeOfArtwork = listTypeOfArtworkRes,
                        StatusName = statusBooking.StatusName,
                        Description = item.Description,
                        Price = item.Price,
                        Image = image.Image,
                        RequestBooking = listRequestBookingRes,
                        CreateDateTime = item.CreateDateTime
                    };
                    listBookingRes.Add(bookingRes);
                }
                return listBookingRes;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<GetBookingForCreatorResDto> GetBookingByBookingIdByCreatorAsync(Guid bookingId)
        {
            try
            {
                if (!_helperService.IsTokenValid())
                {
                    throw new Exception("Invalid Token");
                }
                var accLoggedId = await _accountRepository.GetAccountByIdAsync(_helperService.GetAccIdFromLogged()) ?? throw new Exception("Invalid Token");
                var booking = await _bookingRepository.GetBookingByIdAsync(bookingId) ?? throw new Exception("Booking not found");
                if (booking.CreatorId != accLoggedId.Id)
                {
                    throw new Exception("Not authorized");
                }
                var listTypeOfArtwork = await _bookingArtworkTypeRepository.GetListBookingArtworkTypeByBookingIdAsync(booking.Id) ?? new List<BookingArtworkType>();
                var listRequestArtwork = await _requestArtworkRepository.GetListRequestArtworkByBookingIdAsync(booking.Id) ?? new List<RequestArtwork>();
                List<GetTypeOfArtworkResDto> listTypeOfArtworkRes = new();
                foreach (var item in listTypeOfArtwork)
                {
                    var typeOfArtwork = await _typeOfArtworkRepository.GetTypeOfArtworkByIdAsync(item.TypeOfArtworkId) ?? new TypeOfArtwork();
                    GetTypeOfArtworkResDto typeOfArtworkRes = new()
                    {
                        Id = typeOfArtwork.Id,
                        Type = typeOfArtwork.Type,
                        TypeDescription = typeOfArtwork.TypeDescription
                    };
                    listTypeOfArtworkRes.Add(typeOfArtworkRes);
                }
                List<GetRequestBookingResDto> listRequestBookingRes = new();
                foreach (var item in listRequestArtwork)
                {
                    var status = await _statusRepository.GetStatusByStatusIDAsync(item.StatusId) ?? throw new Exception("Status not found");
                    var bookingArtworkByRequest = await _bookingArtworkRepository.GetBookingArtworkByRequestArtworkIdAsync(item.Id) ?? new BookingArtwork();
                    var imageRequest = await _artworkRepository.GetArtworkByIdAsync(bookingArtworkByRequest.ArtworkId) ?? new Artwork();
                    GetRequestBookingResDto requestBookingRes = new()
                    {
                        RequestBookingId = item.Id,
                        Description = item.Description ?? "",
                        StatusName = status.StatusName,
                        Image = imageRequest.Image,
                        CreateDateTime = item.CreateDateTime
                    };
                    listRequestBookingRes.Add(requestBookingRes);
                }
                var bookingArtworkByBooking = await _bookingArtworkRepository.GetBookingArtworkByBookingIdAsync(booking.Id) ?? new BookingArtwork();
                var image = await _artworkRepository.GetArtworkByIdAsync(bookingArtworkByBooking.ArtworkId) ?? new Artwork();
                var statusBooking = await _statusRepository.GetStatusByStatusIDAsync(booking.StatusId) ?? throw new Exception("Status not found");
                var user = await _accountRepository.GetAccountByIdAsync(booking.UserId) ?? throw new Exception("User not found");
                GetBookingForCreatorResDto bookingRes = new()
                {
                    BookingId = booking.Id,
                    UserName = user.FirstName + " " + user.LastName,
                    ListTypeOfArtwork = listTypeOfArtworkRes,
                    StatusName = statusBooking.StatusName,
                    Description = booking.Description,
                    Price = booking.Price,
                    Image = image.Image,
                    RequestBooking = listRequestBookingRes,
                    CreateDateTime = booking.CreateDateTime
                };
                return bookingRes;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> ChangeStatusBookingByCreatorAsync(ChangeStatusBookingByCreatorReqDto changeStatusBookingByCreatorReqDto)
        {
            try
            {
                if (!_helperService.IsTokenValid())
                {
                    throw new Exception("Invalid Token");
                }
                var accLoggedId = await _accountRepository.GetAccountByIdAsync(_helperService.GetAccIdFromLogged()) ?? throw new Exception("Invalid Token");
                var booking = await _bookingRepository.GetBookingByIdAsync(changeStatusBookingByCreatorReqDto.BookingId) ?? throw new Exception("Booking not found");
                if (booking.CreatorId != accLoggedId.Id)
                {
                    throw new Exception("Not authorized");
                }
                var statusAccept = await _statusRepository.GetStatusByNameAsync(BookingStatusEnum.ACCEPTED) ?? throw new Exception("Status not found");
                var statusReject = await _statusRepository.GetStatusByNameAsync(BookingStatusEnum.REJECTED) ?? throw new Exception("Status not found");

                if (changeStatusBookingByCreatorReqDto.IsAccept)
                {
                    booking.StatusId = statusAccept.Id;
                }
                else
                {
                    booking.StatusId = statusReject.Id;
                }
                return await _bookingRepository.UpdateBookingAsync(booking);
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<List<GetBookingForAdminResDto>> GetListBookingByAdminAsync()
        {
            try
            {
                if (!_helperService.IsTokenValid())
                {
                    throw new Exception("Invalid Token");
                }
                var accLoggedId = await _accountRepository.GetAccountByIdAsync(_helperService.GetAccIdFromLogged()) ?? throw new Exception("Invalid Token");
                var role = await _roleRepository.GetRoleByRoleIDAsync(accLoggedId.RoleId);
                if (!role!.RoleName.Equals("ADMIN")) throw new Exception("Not authorized");
                var listBooking = await _bookingRepository.GetListBookingByAdminIdAsync();
                List<GetBookingForAdminResDto> listBookingRes = new();
                foreach (var item in listBooking)
                {
                    var listTypeOfArtwork = await _bookingArtworkTypeRepository.GetListBookingArtworkTypeByBookingIdAsync(item.Id) ?? new List<BookingArtworkType>();
                    var listRequestArtwork = await _requestArtworkRepository.GetListRequestArtworkByBookingIdAsync(item.Id) ?? new List<RequestArtwork>();
                    List<GetTypeOfArtworkResDto> listTypeOfArtworkRes = new();
                    foreach (var item1 in listTypeOfArtwork)
                    {
                        var typeOfArtwork = await _typeOfArtworkRepository.GetTypeOfArtworkByIdAsync(item1.TypeOfArtworkId) ?? new TypeOfArtwork();
                        GetTypeOfArtworkResDto typeOfArtworkRes = new()
                        {
                            Id = typeOfArtwork.Id,
                            Type = typeOfArtwork.Type,
                            TypeDescription = typeOfArtwork.TypeDescription
                        };
                        listTypeOfArtworkRes.Add(typeOfArtworkRes);
                    }
                    List<GetRequestBookingResDto> listRequestBookingRes = new();
                    foreach (var item2 in listRequestArtwork)
                    {
                        var status = await _statusRepository.GetStatusByStatusIDAsync(item2.StatusId) ?? throw new Exception("Status not found");
                        var bookingArtworkByRequest = await _bookingArtworkRepository.GetBookingArtworkByRequestArtworkIdAsync(item2.Id) ?? new BookingArtwork();
                        var imageRequest = await _artworkRepository.GetArtworkByIdAsync(bookingArtworkByRequest.ArtworkId) ?? new Artwork();
                        GetRequestBookingResDto requestBookingRes = new()
                        {
                            RequestBookingId = item2.Id,
                            Description = item2.Description ?? "",
                            StatusName = status.StatusName,
                            Image = imageRequest.Image,
                            CreateDateTime = item2.CreateDateTime
                        };
                        listRequestBookingRes.Add(requestBookingRes);
                    }
                    var bookingArtworkByBooking = await _bookingArtworkRepository.GetBookingArtworkByBookingIdAsync(item.Id) ?? new BookingArtwork();
                    var image = await _artworkRepository.GetArtworkByIdAsync(bookingArtworkByBooking.ArtworkId) ?? new Artwork();
                    var statusBooking = await _statusRepository.GetStatusByStatusIDAsync(item.StatusId) ?? throw new Exception("Status not found");
                    var user = await _accountRepository.GetAccountByIdAsync(item.UserId) ?? throw new Exception("User not found");
                    var creator = await _accountRepository.GetAccountByIdAsync(item.CreatorId) ?? throw new Exception("Creator not found");

                    GetBookingForAdminResDto bookingRes = new()
                    {
                        BookingId = item.Id,
                        CustomerName = user.FirstName + " " + user.LastName,
                        CreatorName = creator.FirstName + " " + creator.LastName,
                        ListTypeOfArtwork = listTypeOfArtworkRes,
                        StatusName = statusBooking.StatusName,
                        Description = item.Description,
                        Price = item.Price,
                        Image = image.Image,
                        RequestBooking = listRequestBookingRes,
                        CreateDateTime = item.CreateDateTime
                    };
                    listBookingRes.Add(bookingRes);
                }
                return listBookingRes;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> ChangeStatusRequestByCreatorAsync(ChangeStatusRequestByCreatorResDto changeStatusRequestByCreatorResDto)
        {
            try
            {
                if (!_helperService.IsTokenValid())
                {
                    throw new Exception("Invalid Token");
                }
                var accLoggedId = await _accountRepository.GetAccountByIdAsync(_helperService.GetAccIdFromLogged()) ?? throw new Exception("Invalid Token");
                var requestArtwork = await _requestArtworkRepository.GetRequestArtworkByRequestArtworkId(changeStatusRequestByCreatorResDto.RequestBookingId) ?? throw new Exception("Request Artwork not found");
                var booking = await _bookingRepository.GetBookingByIdAsync(requestArtwork.BookingId) ?? throw new Exception("Booking not found");
                if (booking.CreatorId != accLoggedId.Id)
                {
                    throw new Exception("Not authorized");
                }
                var statusAccept = await _statusRepository.GetStatusByNameAsync(RequestArtworkStatusEnum.ACCEPTED) ?? throw new Exception("Status not found");
                var statusReject = await _statusRepository.GetStatusByNameAsync(RequestArtworkStatusEnum.REJECTED) ?? throw new Exception("Status not found");
                if (changeStatusRequestByCreatorResDto.IsAccept)
                {
                    requestArtwork.StatusId = statusAccept.Id;
                }
                else
                {
                    requestArtwork.StatusId = statusReject.Id;
                }
                return await _requestArtworkRepository.UpdateRequestArtworkAsync(requestArtwork);

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> ChangeStatusRequestBookingByCustomerAsync(ChangeStatusRequestBookingByCustomerReqDto changeStatusRequestBookingByCustomerReqDto)
        {
            try
            {
                if (!_helperService.IsTokenValid())
                {
                    throw new Exception("Invalid Token");
                }
                var accLoggedId = await _accountRepository.GetAccountByIdAsync(_helperService.GetAccIdFromLogged()) ?? throw new Exception("Invalid Token");
                if (changeStatusRequestBookingByCustomerReqDto.BookingId != Guid.Empty)
                {
                    var booking = await _bookingRepository.GetBookingByIdAsync(changeStatusRequestBookingByCustomerReqDto.BookingId ?? throw new Exception("Booking not found")) ?? throw new Exception("Booking not found");
                    if (booking.UserId != accLoggedId.Id)
                    {
                        throw new Exception("Not authorized");
                    }
                    if (changeStatusRequestBookingByCustomerReqDto.IsAccept)
                    {
                        var statusAccept = await _statusRepository.GetStatusByNameAsync(BookingStatusEnum.DONE) ?? throw new Exception("Status not found");
                        booking.StatusId = statusAccept.Id;
                        // Update balance for creator and customer
                        var creator = await _accountRepository.GetAccountByIdAsync(booking.CreatorId) ?? throw new Exception("Creator not found");
                        var customer = await _accountRepository.GetAccountByIdAsync(booking.UserId) ?? throw new Exception("Customer not found");
                        if (customer.Balance < booking.Price)
                        {
                            throw new Exception("Balance not enough");
                        }
                        creator.Balance += booking.Price;
                        customer.Balance -= booking.Price;
                        await _accountRepository.UpdateAccountAsync(creator);
                        await _accountRepository.UpdateAccountAsync(customer);
                        // Update artwork is owned by customer
                        var statusOrder = await _statusRepository.GetStatusByNameAsync(OrderStatusEnum.PAID) ?? throw new Exception("Status not found");
                        Order newOrder = new()
                        {
                            AccountId = customer.Id,
                            Payment = "Balance",
                            StatusId = statusOrder.Id,                            
                        };
                        var order = await _orderRepository.CreateOrderAsync(newOrder);
                        var bookingArtwork = await _bookingArtworkRepository.GetBookingArtworkByBookingIdAsync(booking.Id) ?? throw new Exception("Booking Artwork not found");
                        var artwork = await _artworkRepository.GetArtworkByIdAsync(bookingArtwork.ArtworkId) ?? throw new Exception("Artwork not found");
                        artwork.OrderId = order.Id;
                        await _artworkRepository.UpdateArtworkAsync(artwork);                       
                        return true;
                    }
                    else
                    {
                        var statusReject = await _statusRepository.GetStatusByNameAsync(BookingStatusEnum.CANCEL) ?? throw new Exception("Status not found");
                        booking.StatusId = statusReject.Id;
                        await _bookingRepository.UpdateBookingAsync(booking);
                        return true;
                    }                  
                } else if (changeStatusRequestBookingByCustomerReqDto.RequestBookingId != Guid.Empty)
                {
                    var requestArtwork = await _requestArtworkRepository.GetRequestArtworkByRequestArtworkId(changeStatusRequestBookingByCustomerReqDto.RequestBookingId ?? throw new Exception("Request Artwork not found")) ?? throw new Exception("Request Artwork not found");
                    var booking = await _bookingRepository.GetBookingByIdAsync(requestArtwork.BookingId) ?? throw new Exception("Booking not found");
                    if (booking.UserId != accLoggedId.Id)
                    {
                        throw new Exception("Not authorized");
                    }
                    if (changeStatusRequestBookingByCustomerReqDto.IsAccept)
                    {
                        var statusAccept = await _statusRepository.GetStatusByNameAsync(RequestArtworkStatusEnum.DONE) ?? throw new Exception("Status not found");
                        requestArtwork.StatusId = statusAccept.Id;
                        // Update balance for creator and customer
                        var creator = await _accountRepository.GetAccountByIdAsync(booking.CreatorId) ?? throw new Exception("Creator not found");
                        var customer = await _accountRepository.GetAccountByIdAsync(booking.UserId) ?? throw new Exception("Customer not found");
                        if (customer.Balance < booking.Price)
                        {
                            throw new Exception("Balance not enough");
                        }
                        creator.Balance += booking.Price;
                        customer.Balance -= booking.Price;
                        await _accountRepository.UpdateAccountAsync(creator);
                        await _accountRepository.UpdateAccountAsync(customer);

                        // Update artwork is owned by customer
                        var statusOrder = await _statusRepository.GetStatusByNameAsync(OrderStatusEnum.PAID) ?? throw new Exception("Status not found");
                        Order newOrder = new()
                        {
                            AccountId = customer.Id,
                            Payment = "Balance",
                            StatusId = statusOrder.Id,
                        };
                        var order = await _orderRepository.CreateOrderAsync(newOrder);
                        var bookingArtwork = await _bookingArtworkRepository.GetBookingArtworkByBookingIdAsync(booking.Id) ?? throw new Exception("Booking Artwork not found");
                        var artwork = await _artworkRepository.GetArtworkByIdAsync(bookingArtwork.ArtworkId) ?? throw new Exception("Artwork not found");
                        artwork.OrderId = order.Id;
                        await _artworkRepository.UpdateArtworkAsync(artwork);
                        return true;
                    }
                    else
                    {
                        var statusReject = await _statusRepository.GetStatusByNameAsync(RequestArtworkStatusEnum.REJECTED) ?? throw new Exception("Status not found");
                        requestArtwork.StatusId = statusReject.Id;
                        await _requestArtworkRepository.UpdateRequestArtworkAsync(requestArtwork);
                        return true;
                    }
                } else
                {
                    throw new Exception("BookingId or RequestBookingId is required");
                }                
            } catch (Exception)
            {
                throw;
            }
        }
    }
}
