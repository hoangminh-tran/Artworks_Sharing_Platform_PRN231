using Artworks_Sharing_Plaform_Api.Enum;
using Artworks_Sharing_Plaform_Api.Model;
using Artworks_Sharing_Plaform_Api.Model.Dto.ReqDto;
using Artworks_Sharing_Plaform_Api.Model.Dto.ResDto;
using Artworks_Sharing_Plaform_Api.Repository.Interface;
using Artworks_Sharing_Plaform_Api.Service.Interface;

namespace Artworks_Sharing_Plaform_Api.Service
{
    public class TypeOfArtworkService : ITypeOfArtworkService
    {
        private readonly ITypeOfArtworkRepository _typeOfArtworkRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IStatusRepository _statusRepository;
        private readonly IHelpperService _helperService;
        private readonly IRoleRepository _roleRepository;
        private readonly IStatusService _statusService;

        public TypeOfArtworkService(ITypeOfArtworkRepository typeOfArtworkRepository, IAccountRepository accountRepository, IStatusRepository statusRepository, IHelpperService helperService, IRoleRepository roleRepository, IStatusService statusService)
        {
            _typeOfArtworkRepository = typeOfArtworkRepository;
            _accountRepository = accountRepository;
            _statusRepository = statusRepository;
            _helperService = helperService;
            _roleRepository = roleRepository;
            _statusService = statusService;
        }

        public async Task<bool> CreateTypeOfArtworkAsync(IFormFile? file, TypeOfArtworkReqDto dto)
        {
            try
            {
                if (!_helperService.IsTokenValid())
                {
                    throw new Exception(ServerErrorEnum.NOT_AUTHENTICATED);
                }
                var accLoggedId = await _accountRepository.GetAccountByIdAsync(_helperService.GetAccIdFromLogged()) ?? throw new Exception(ServerErrorEnum.NOT_AUTHENTICATED);
                var adminRole = await _roleRepository.GetRoleByNameAsync(RoleEnum.ADMIN) ?? throw new Exception(ServerErrorEnum.SERVER_ERROR);
                var creatorRole = await _roleRepository.GetRoleByNameAsync(RoleEnum.CREATOR) ?? throw new Exception(ServerErrorEnum.SERVER_ERROR);
                if (accLoggedId.RoleId != adminRole.Id && accLoggedId.RoleId != creatorRole.Id)
                {
                    throw new Exception(ServerErrorEnum.NOT_AUTHORIZED);
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
                var status = await _statusService.GetStatusByStatusName("ACTIVE");
                if (accLoggedId.RoleId == adminRole.Id)
                {
                    status = await _statusService.GetStatusByStatusName("ACTIVE");
                }
                else if (accLoggedId.RoleId == creatorRole.Id)
                {
                    status = await _statusService.GetStatusByStatusName("PENDING");
                }
                if (status == null)
                {
                    throw new Exception(TypeOfArtworkErrorEnum.TYPE_OF_ARTWORK_STATUS_NOT_FOUND);
                }
                TypeOfArtwork typeOfArtwork = new()
                {
                    Type = dto.type,
                    TypeDescription = dto.typeDescription,
                    TypeImageDeafault = imageBytes,
                    StatusId = status.Id                    
                };
                return await _typeOfArtworkRepository.CreateTypeOfArtwork(typeOfArtwork);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<GetTypeOfArtworkResDto>> GetListTypeOfArtworkAsync()
        {
            try
            {
                var listTypeOfArtwork = await _typeOfArtworkRepository.GetListTypeOfArtworkAsync();
                // Filler DeleteDateTime if have then remove from list
                listTypeOfArtwork = listTypeOfArtwork.Where(type => type.DeleteDateTime == null).ToList();
                List<GetTypeOfArtworkResDto> listTypeOfArtworkResDto = [];
                foreach (var item in listTypeOfArtwork)
                {
                    listTypeOfArtworkResDto.Add(new GetTypeOfArtworkResDto
                    {
                        Id = item.Id,
                        Type = item.Type,
                        TypeDescription = item.TypeDescription,                        
                    });
                }
                return listTypeOfArtworkResDto;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<GetTypeOfArtworkResDto>> GetListTypeOfArtworkAsyncByRoleAdmin()
        {
            try
            {
                if (!_helperService.IsTokenValid())
                {
                    throw new Exception(ServerErrorEnum.NOT_AUTHENTICATED);
                }
                var accLoggedId = await _accountRepository.GetAccountByIdAsync(_helperService.GetAccIdFromLogged()) ?? throw new Exception(ServerErrorEnum.NOT_AUTHENTICATED);
                var role = await _roleRepository.GetRoleByNameAsync(RoleEnum.ADMIN) ?? throw new Exception(ServerErrorEnum.SERVER_ERROR);
                if (accLoggedId.RoleId != role.Id)
                {
                    throw new Exception(ServerErrorEnum.NOT_AUTHORIZED);
                }  
                var status = await _statusService.GetStatusByStatusName("PENDING") ?? throw new Exception(ServerErrorEnum.STATUS_NOT_FOUND);
                var listTypeOfArtwork = await _typeOfArtworkRepository.GetListTypeOfArtworkAsync();   
                listTypeOfArtwork = listTypeOfArtwork.Where(type => type.StatusId != status.Id).ToList();
                List<GetTypeOfArtworkResDto> listTypeOfArtworkResDto = [];
                foreach (var item in listTypeOfArtwork)
                {
                    listTypeOfArtworkResDto.Add(new GetTypeOfArtworkResDto
                    {
                        Id = item.Id,
                        Type = item.Type,
                        TypeDescription = item.TypeDescription,
                        statusName = (item.Status?? new Status()).StatusName,
                    });
                }
                return listTypeOfArtworkResDto;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> UpdateTypeOfArtworkAsync(UpdateTypeOfArtworkReqDto dto)
        {
            try
            {
                if (!_helperService.IsTokenValid())
                {
                    throw new Exception(ServerErrorEnum.NOT_AUTHENTICATED);
                }
                var accLoggedId = await _accountRepository.GetAccountByIdAsync(_helperService.GetAccIdFromLogged()) ?? throw new Exception(ServerErrorEnum.NOT_AUTHENTICATED);
                var role = await _roleRepository.GetRoleByNameAsync(RoleEnum.ADMIN) ?? throw new Exception(ServerErrorEnum.SERVER_ERROR);
                if (accLoggedId.RoleId != role.Id)
                {
                    throw new Exception(ServerErrorEnum.NOT_AUTHORIZED);
                }             

                var typeOfArtworkExist = await _typeOfArtworkRepository.GetTypeOfArtworkByIdAsync(dto.typeOfArtworkID);
                if (typeOfArtworkExist == null)
                {
                    throw new Exception(TypeOfArtworkErrorEnum.TYPE_OF_ARTWORK_NOT_FOUND);
                }

                if (!String.IsNullOrEmpty(dto.type?.Trim()))
                {
                    typeOfArtworkExist.Type = dto.type;
                }
                if (!String.IsNullOrEmpty(dto.typeDescription?.Trim()))
                {
                    typeOfArtworkExist.TypeDescription = dto.typeDescription;
                }

                return await _typeOfArtworkRepository.UpdateTypeOfArtwork(typeOfArtworkExist);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> DeleteTypeOfArtworkAsync(Guid id)
        {
            try
            {
                if (!_helperService.IsTokenValid())
                {
                    throw new Exception(ServerErrorEnum.NOT_AUTHENTICATED);
                }
                var accLoggedId = await _accountRepository.GetAccountByIdAsync(_helperService.GetAccIdFromLogged()) ?? throw new Exception(ServerErrorEnum.NOT_AUTHENTICATED);
                var status = await _statusService.GetStatusByStatusName("DEACTIVE");
                var role = await _roleRepository.GetRoleByNameAsync(RoleEnum.ADMIN) ?? throw new Exception(ServerErrorEnum.SERVER_ERROR);
                if (accLoggedId.RoleId != role.Id)
                {
                    throw new Exception(ServerErrorEnum.NOT_AUTHORIZED);
                }               
                var typeOfArtworkExist = await _typeOfArtworkRepository.GetTypeOfArtworkByIdAsync(id);
                if (typeOfArtworkExist == null)
                {
                    throw new Exception(TypeOfArtworkErrorEnum.TYPE_OF_ARTWORK_NOT_FOUND);
                }
                if (status == null)
                {
                    throw new Exception(TypeOfArtworkErrorEnum.TYPE_OF_ARTWORK_STATUS_NOT_FOUND);
                }
                typeOfArtworkExist.DeleteDateTime = DateTime.Now;
                typeOfArtworkExist.StatusId = status.Id;
                return await _typeOfArtworkRepository.UpdateTypeOfArtwork(typeOfArtworkExist);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> ActiveTypeOfArtworkAsync(Guid id)
        {
            try
            {
                if (!_helperService.IsTokenValid())
                {
                    throw new Exception(ServerErrorEnum.NOT_AUTHENTICATED);
                }
                var accLoggedId = await _accountRepository.GetAccountByIdAsync(_helperService.GetAccIdFromLogged()) ?? throw new Exception(ServerErrorEnum.NOT_AUTHENTICATED);
                var status = await _statusService.GetStatusByStatusName(TypeOfArtworkEnum.ACTIVE);
                var role = await _roleRepository.GetRoleByNameAsync(RoleEnum.ADMIN) ?? throw new Exception(ServerErrorEnum.SERVER_ERROR);
                if (accLoggedId.RoleId != role.Id)
                {
                    throw new Exception(ServerErrorEnum.NOT_AUTHORIZED);
                }
                var typeOfArtwork = await _typeOfArtworkRepository.GetTypeOfArtworkByIdAsync(id) ?? throw new Exception(TypeOfArtworkErrorEnum.TYPE_OF_ARTWORK_NOT_FOUND);
                typeOfArtwork.DeleteDateTime = null;
                typeOfArtwork.StatusId = status.Id;
                return await _typeOfArtworkRepository.UpdateTypeOfArtwork(typeOfArtwork);
            } catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> DeActiveTypeOfArtworkAsync(Guid id)
        {
            try
            {
                if (!_helperService.IsTokenValid())
                {
                    throw new Exception(ServerErrorEnum.NOT_AUTHENTICATED);
                }
                var accLoggedId = await _accountRepository.GetAccountByIdAsync(_helperService.GetAccIdFromLogged()) ?? throw new Exception(ServerErrorEnum.NOT_AUTHENTICATED);
                var status = await _statusService.GetStatusByStatusName(TypeOfArtworkEnum.DEACTIVE);
                var role = await _roleRepository.GetRoleByNameAsync(RoleEnum.ADMIN) ?? throw new Exception(ServerErrorEnum.SERVER_ERROR);
                if (accLoggedId.RoleId != role.Id)
                {
                    throw new Exception(ServerErrorEnum.NOT_AUTHORIZED);
                }
                var typeOfArtwork = await _typeOfArtworkRepository.GetTypeOfArtworkByIdAsync(id) ?? throw new Exception(TypeOfArtworkErrorEnum.TYPE_OF_ARTWORK_NOT_FOUND);
                typeOfArtwork.DeleteDateTime = null;
                typeOfArtwork.StatusId = status.Id;
                return await _typeOfArtworkRepository.UpdateTypeOfArtwork(typeOfArtwork);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> ChangeStatusTypeOfArtworkByAdminAsync(ChangeStatusRequestDto dto)
        {
            try
            {
                if (!_helperService.IsTokenValid())
                {
                    throw new Exception(ServerErrorEnum.NOT_AUTHENTICATED);
                }
                var accLoggedId = await _accountRepository.GetAccountByIdAsync(_helperService.GetAccIdFromLogged()) ?? throw new Exception(ServerErrorEnum.NOT_AUTHENTICATED);
                var status = await _statusService.GetStatusByStatusName(dto.StatusName.ToUpper());
                if(status == null)
                {
                    throw new Exception(ServerErrorEnum.SERVER_ERROR);
                }
                var role = await _roleRepository.GetRoleByNameAsync(RoleEnum.ADMIN) ?? throw new Exception(ServerErrorEnum.SERVER_ERROR);
                if (accLoggedId.RoleId != role.Id)
                {
                    throw new Exception(ServerErrorEnum.NOT_AUTHORIZED);
                }
                var typeOfArtwork = await _typeOfArtworkRepository.GetTypeOfArtworkByIdAsync(dto.Id) ?? throw new Exception(TypeOfArtworkErrorEnum.TYPE_OF_ARTWORK_NOT_FOUND);

                typeOfArtwork.DeleteDateTime = (dto.StatusName.ToUpper() == TypeOfArtworkEnum.DEACTIVE) ? DateTime.Now : null;
                typeOfArtwork.StatusId = status.Id;

                return await _typeOfArtworkRepository.UpdateTypeOfArtwork(typeOfArtwork);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<GetTypeOfArtworkResDto> GetTypeOfArtworkAsyncById(Guid id)
        {
            try
            {
                var typeOfArtwork = await _typeOfArtworkRepository.GetTypeOfArtworkByIdAsync(id);
                return new GetTypeOfArtworkResDto
                {
                    Id = typeOfArtwork.Id,
                    Type = typeOfArtwork.Type,
                    TypeDescription = typeOfArtwork.TypeDescription,
                    statusName = typeOfArtwork.Status.StatusName,
                };
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<GetTypeOfArtworkResDto>> GetListTypeOfArtworkAsyncByRoleCreator()
        {
            try
            {
                if (!_helperService.IsTokenValid())
                {
                    throw new Exception(ServerErrorEnum.NOT_AUTHENTICATED);
                }
                var accLoggedId = await _accountRepository.GetAccountByIdAsync(_helperService.GetAccIdFromLogged()) ?? throw new Exception(ServerErrorEnum.NOT_AUTHENTICATED);
                var status = await _statusService.GetStatusByStatusName(StatusEnum.ACTIVE);
                var role = await _roleRepository.GetRoleByNameAsync(RoleEnum.CREATOR) ?? throw new Exception(ServerErrorEnum.SERVER_ERROR);
                if (accLoggedId.RoleId != role.Id)
                {
                    throw new Exception(ServerErrorEnum.NOT_AUTHORIZED);
                }
                var listTypeOfArtwork = await _typeOfArtworkRepository.GetListTypeOfArtworkAsync();
                List<GetTypeOfArtworkResDto> listTypeOfArtworkResponse = new ();
                if (listTypeOfArtwork.Count == 0)
                {
                    return listTypeOfArtworkResponse;
                }
                foreach (var item in listTypeOfArtwork)
                {
                    listTypeOfArtworkResponse.Add(new GetTypeOfArtworkResDto
                    {
                        Id = item.Id,
                        Type = item.Type,
                        TypeDescription = item.TypeDescription,
                        statusName = item.Status.StatusName,
                    });
                }
                return listTypeOfArtworkResponse;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<GetTypeOfArtworkResDto>> GetListTypeOfArtworkAsyncByTypeOfArtworkNameAndRoleCreator(string type)
        {
            try
            {
                if (!_helperService.IsTokenValid())
                {
                    throw new Exception(ServerErrorEnum.NOT_AUTHENTICATED);
                }
                var accLoggedId = await _accountRepository.GetAccountByIdAsync(_helperService.GetAccIdFromLogged()) ?? throw new Exception(ServerErrorEnum.NOT_AUTHENTICATED);
                var status = await _statusService.GetStatusByStatusName(StatusEnum.ACTIVE);
                var role = await _roleRepository.GetRoleByNameAsync(RoleEnum.CREATOR) ?? throw new Exception(ServerErrorEnum.SERVER_ERROR);
                if (accLoggedId.RoleId != role.Id)
                {
                    throw new Exception(ServerErrorEnum.NOT_AUTHORIZED);
                }
                var listTypeOfArtwork = await _typeOfArtworkRepository.GetListTypeOfArtworkByTypeOfArtworkNameAsync(type);
                List<GetTypeOfArtworkResDto> listTypeOfArtworkResponse = new ();
                if(listTypeOfArtwork.Count == 0)
                {
                    return listTypeOfArtworkResponse;
                }
                foreach (var item in listTypeOfArtwork)
                {
                    listTypeOfArtworkResponse.Add(new GetTypeOfArtworkResDto
                    {
                        Id = item.Id,
                        Type = item.Type,
                        TypeDescription = item.TypeDescription,
                        statusName = item.Status.StatusName,
                    });
                }
                return listTypeOfArtworkResponse;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<GetTypeOfArtworkResDto>> GetListRequestTypeOfArtwork()
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
                var listTypeOfArtwork = /*await _typeOfArtworkRepository.GetListRequestTypeOfArtworkAsync(accLoggedId.Id);*/ new List<TypeOfArtwork>();
                // Filler DeleteDateTime if have then remove from list
                listTypeOfArtwork = listTypeOfArtwork.Where(type => type.DeleteDateTime == null).ToList();
                List<GetTypeOfArtworkResDto> listTypeOfArtworkResDto = [];
                foreach (var item in listTypeOfArtwork)
                {
                    listTypeOfArtworkResDto.Add(new GetTypeOfArtworkResDto
                    {
                        Id = item.Id,
                        Type = item.Type,
                        TypeDescription = item.TypeDescription,
                    });
                }
                return listTypeOfArtworkResDto;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<GetTypeOfArtworkResDto>> GetListPendingTypeOfArtworkAsync()
        {
            try
            {
                if (!_helperService.IsTokenValid())
                {
                    throw new Exception(ServerErrorEnum.NOT_AUTHENTICATED);
                }
                var accLoggedId = await _accountRepository.GetAccountByIdAsync(_helperService.GetAccIdFromLogged()) ?? throw new Exception(ServerErrorEnum.NOT_AUTHENTICATED);
                var role = await _roleRepository.GetRoleByNameAsync(RoleEnum.ADMIN) ?? throw new Exception(ServerErrorEnum.SERVER_ERROR);
                if (accLoggedId.RoleId != role.Id)
                {
                    throw new Exception(ServerErrorEnum.NOT_AUTHORIZED);
                }
                var listTypeOfArtwork = await _typeOfArtworkRepository.GetListTypeOfArtworkAsync();
                // Filler DeleteDateTime if have then remove from list
                listTypeOfArtwork = listTypeOfArtwork.Where(type => type.DeleteDateTime == null).ToList();
                var statusPending = await _statusService.GetStatusByStatusName("PENDING") ?? throw new Exception(ServerErrorEnum.SERVER_ERROR);
                listTypeOfArtwork = listTypeOfArtwork.Where(type => type.StatusId == statusPending.Id).ToList();
                List<GetTypeOfArtworkResDto> listTypeOfArtworkResDto = [];
                foreach (var item in listTypeOfArtwork)
                {
                    listTypeOfArtworkResDto.Add(new GetTypeOfArtworkResDto
                    {
                        Id = item.Id,
                        Type = item.Type,
                        TypeDescription = item.TypeDescription,
                        statusName = item.Status.StatusName,
                    });
                }
                return listTypeOfArtworkResDto;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}