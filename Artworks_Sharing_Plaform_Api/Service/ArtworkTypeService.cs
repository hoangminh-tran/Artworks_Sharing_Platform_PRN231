using Artworks_Sharing_Plaform_Api.Enum;
using Artworks_Sharing_Plaform_Api.Model;
using Artworks_Sharing_Plaform_Api.Model.Dto.ReqDto;
using Artworks_Sharing_Plaform_Api.Model.Dto.ResDto;
using Artworks_Sharing_Plaform_Api.Repository.Interface;
using Artworks_Sharing_Plaform_Api.Service.Interface;
using System.Data;

namespace Artworks_Sharing_Plaform_Api.Service
{
    public class ArtworkTypeService : IArtworkTypeService
    {
        private readonly IArtworkTypeRepository _artworkTypeRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IStatusRepository _statusRepository;
        private readonly IHelpperService _helperService;
        private readonly IRoleRepository _roleRepository;
        private readonly ITypeOfArtworkRepository _typeOfArtworkRepository;
        private readonly IArtworkRepository _artworkRepository;
        public ArtworkTypeService(IArtworkTypeRepository artworkTypeRepository, IAccountRepository accountRepository, IStatusRepository statusRepository, IHelpperService helperService, IRoleRepository roleRepository, ITypeOfArtworkRepository typeOfArtworkRepository, IArtworkRepository artworkRepository)
        {
            _artworkTypeRepository = artworkTypeRepository;
            _accountRepository = accountRepository;
            _statusRepository = statusRepository;
            _helperService = helperService;
            _roleRepository = roleRepository;
            _typeOfArtworkRepository = typeOfArtworkRepository;
            _artworkRepository = artworkRepository;
        }

        public async Task<bool> CreateArtworkTypeByListTypeOfArtworkAsync(CreateAndUpdateListArtworkTypeReqDto reqDto)
        {
            try
            {
                if (!_helperService.IsTokenValid())
                {
                    throw new Exception(ServerErrorEnum.NOT_AUTHENTICATED);
                }
                var accLoggedId = await _accountRepository.GetAccountByIdAsync(_helperService.GetAccIdFromLogged()) ?? throw new Exception(ServerErrorEnum.NOT_AUTHENTICATED);
                
                // Check is creator of this artwork
                var Artwork = await _artworkRepository.GetArtworkByArtworkByIdAsync(reqDto.ArtworkId) ?? throw new Exception(ArtworkTypeErrorNum.ARTWORK_NOT_FOUND);
                if (Artwork.CreatorId != accLoggedId.Id)
                {
                    throw new Exception(ServerErrorEnum.NOT_AUTHORIZED);
                }

                foreach(var id in reqDto.ListTypeOfArtworkId)
                {
                    if ((await _typeOfArtworkRepository.GetTypeOfArtworkByIdAsync(id)) == null)
                    {
                        throw new Exception(ArtworkTypeErrorNum.TYPE_OF_ARTWORK_NOT_FOUND);
                    }
                }

                foreach (var id in reqDto.ListTypeOfArtworkId)
                {
                    ArtworkType artwork = new()
                    {
                        ArtworkId = reqDto.ArtworkId,
                        TypeOfArtworkId = id
                    };
                    await _artworkTypeRepository.CreateArtworkTypeAsync(artwork);
                }
                return true;
            }
            catch (Exception)
            {                
                throw;
            }
        }

        public async Task<bool> UpdateArtworkTypeByListTypeOfArtworkAsync(CreateAndUpdateListArtworkTypeReqDto reqDto)
        {
            try
            {
                if (!_helperService.IsTokenValid())
                {
                    throw new Exception(ServerErrorEnum.NOT_AUTHENTICATED);
                }
                var accLoggedId = await _accountRepository.GetAccountByIdAsync(_helperService.GetAccIdFromLogged()) ?? throw new Exception(ServerErrorEnum.NOT_AUTHENTICATED);
                var Artwork = await _artworkRepository.GetArtworkByArtworkByIdAsync(reqDto.ArtworkId) ?? throw new Exception(ArtworkTypeErrorNum.ARTWORK_NOT_FOUND);
                if (Artwork.CreatorId != accLoggedId.Id)
                {
                    throw new Exception(ServerErrorEnum.NOT_AUTHORIZED);
                }

                foreach (var id in reqDto.ListTypeOfArtworkId)
                {
                    if ((await _typeOfArtworkRepository.GetTypeOfArtworkByIdAsync(id)) == null)
                    {
                        throw new Exception(ArtworkTypeErrorNum.TYPE_OF_ARTWORK_NOT_FOUND);
                    }
                }

                // Get all ArtworkType with ArtworkID
                var allArtworkTypes = await _artworkTypeRepository.GetAllArtworkTypeByArtworkIdAsync(reqDto.ArtworkId);
                foreach (var artworkType in allArtworkTypes)
                {
                    artworkType.DeleteDateTime = DateTime.Now;
                    await _artworkTypeRepository.UpdateArtworkTypeAsync(artworkType);
                }
                
                foreach (var id in reqDto.ListTypeOfArtworkId)
                {
                    ArtworkType artwork = new()
                    {
                        ArtworkId = reqDto.ArtworkId,
                        TypeOfArtworkId = id
                    };
                    await _artworkTypeRepository.CreateArtworkTypeAsync(artwork);
                }             

                return true;
            }
            catch (Exception)
            {                
                throw;
            }
        }

        public async Task<List<GetArtworkTypeResponseDto>> GetArtworkTypeAsyncByTypeOfArtworkId(Guid typeOfArtworkId)
        {
            try
            {
                if(typeOfArtworkId == null)
                {
                    throw new Exception(ArtworkTypeErrorNum.ARTWORK_NOT_FOUND);
                }
                var artworkType = await _artworkTypeRepository.GetArtworkTypeAsyncByTypeOfArtworkId(typeOfArtworkId);
                List<GetArtworkTypeResponseDto> artworkTypeListResponse = new ();
                foreach(var at in  artworkType)
                {
                    artworkTypeListResponse.Add(new GetArtworkTypeResponseDto
                    {
                         ArtworkId = at.ArtworkId,
                         TypeOfArtworkId = at.ArtworkId,
                    });
                }
                return artworkTypeListResponse;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
