using Artworks_Sharing_Plaform_Api.Enum;
using Artworks_Sharing_Plaform_Api.Model;
using Artworks_Sharing_Plaform_Api.Model.Dto.ReqDto;
using Artworks_Sharing_Plaform_Api.Model.Dto.ResDto;
using Artworks_Sharing_Plaform_Api.Repository;
using Artworks_Sharing_Plaform_Api.Repository.Interface;
using Artworks_Sharing_Plaform_Api.Service.Interface;
using System.Diagnostics;

namespace Artworks_Sharing_Plaform_Api.Service
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IPostArtworkRepository _postArtworkRepository;
        private readonly IArtworkRepository _artworkRepository;
        private readonly IArtworkTypeRepository _artworkTypeRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly ILikeByRepository _likeByRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IHelpperService _helperService;
        private readonly IStatusRepository _statusRepository;
        private readonly ITypeOfArtworkRepository _typeOfArtworkRepository;        
        
        public PostService(IPostRepository postRepository, IPostArtworkRepository postArtworkRepository, IArtworkRepository artworkRepository, IArtworkTypeRepository artworkTypeRepository, IAccountRepository accountRepository, ILikeByRepository likeByRepository, IHelpperService helperService, IStatusRepository statusRepository, ITypeOfArtworkRepository typeOfArtworkRepository, IRoleRepository roleRepository)
        {
            _postRepository = postRepository;
            _postArtworkRepository = postArtworkRepository;
            _artworkRepository = artworkRepository;
            _artworkTypeRepository = artworkTypeRepository;
            _accountRepository = accountRepository;
            _likeByRepository = likeByRepository;
            _helperService = helperService;
            _statusRepository = statusRepository;
            _typeOfArtworkRepository = typeOfArtworkRepository;
            _roleRepository = roleRepository;
        }

        public async Task<bool> CreatePostAsync(CreatePostReqDto post)
        {
            try
            {
                if(!_helperService.IsTokenValid())
                {
                    throw new Exception(ServerErrorEnum.NOT_AUTHENTICATED);
                }
                var account = await _accountRepository.GetAccountByIdAsync(_helperService.GetAccIdFromLogged()) ?? throw new Exception(AccountErrorEnum.ACCOUNT_NOT_FOUND);
                // Check list guid artwork id is have in database and is public status and is not delete and is not creator
                foreach (var artworkId in post.ListArtwork)
                {
                    var artwork = await _artworkRepository.GetArtworkByIdAsync(artworkId) ?? throw new Exception("ARTWORK_NOT_FOUND");
                    if (artwork.StatusId != (await _statusRepository.GetStatusByNameAsync(ArtworkStatusEnum.PUBLIC) ?? throw new Exception(ServerErrorEnum.SERVER_ERROR)).Id)
                    {
                        throw new Exception("ARTWORK_NOT_PUBLIC");
                    }
                    if (artwork.DeleteDateTime != null)
                    {
                        throw new Exception("ARTWORK_IS_DELETE");
                    }
                    if (artwork.CreatorId != account.Id)
                    {
                        throw new Exception(ServerErrorEnum.NOT_AUTHORIZED);
                    }
                }

                Post createPost = new()
                {
                    ContentPost = post.ContentPost,
                    CreatorId = account.Id
                };
                var newPost = await _postRepository.CreatePostAsync(createPost);

                foreach (var artworkId in post.ListArtwork)
                {
                    PostArtwork postArtwork = new()
                    {
                        PostId = newPost.Id,
                        ArtworkId = artworkId
                    };
                    await _postArtworkRepository.CreatePostArtworkAsync(postArtwork);
                }
                return true;
            } catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<GetPostResDto>> GetListPostAsync()
        {
            try
            {
                var posts = await _postRepository.GetListPostAsync();
                var listPostResDto = new List<GetPostResDto>();
                foreach (var post in posts)
                {
                    var postArtwork = await _postArtworkRepository.GetListPostArtworkByPostIdAsync(post.Id);
                    var creatorPost = await _accountRepository.GetAccountByIdAsync(post.CreatorId) ?? new Account();
                    var likePost = await _likeByRepository.GetListLikeByPostIdAsync(post.Id);
                    var listArtwork = new List<GetArtworkResDto>();
                    foreach (var artwork in postArtwork)
                    {
                        var art = await _artworkRepository.GetArtworkByIdAsync(artwork.ArtworkId) ?? new Artwork();                        
                        var likeArtwork = await _likeByRepository.GetListLikeByArtworkIdAsync(art.Id);                                        
                        var artworkResDto = new GetArtworkResDto
                        {
                            ArtworkId = art.Id,                    
                            Image = art.Image                  
                        };
                        listArtwork.Add(artworkResDto);
                    }
                    var accountLoggedIn = await _accountRepository.GetAccountByIdAsync(_helperService.GetAccIdFromLoogedNotThrow());
                    var postResDto = new GetPostResDto
                    {
                        PostId = post.Id,
                        ContentPost = post.ContentPost,
                        CreateDateTime = post.CreateDateTime,
                        CreatorName = creatorPost.FirstName + " " + creatorPost.LastName,
                        LikeCount = likePost.Count,
                        ListArtwork = listArtwork,
                        IsLike = accountLoggedIn != null ? likePost.Any(l => l.AccountId == accountLoggedIn.Id) : false,
                        CreatorId = creatorPost.Id
                    };
                    listPostResDto.Add(postResDto);
                }
                listPostResDto = listPostResDto.OrderByDescending(p => p.CreateDateTime).ToList();
                return listPostResDto;
            } catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<GetPostResDto>> GetListPostByCustomerAsync()
        {
            try
            {
                if (!_helperService.IsTokenValid())
                {
                    throw new Exception(ServerErrorEnum.NOT_AUTHENTICATED);
                }
                var accLoggedId = await _accountRepository.GetAccountByIdAsync(_helperService.GetAccIdFromLogged()) ?? throw new Exception(ServerErrorEnum.NOT_AUTHENTICATED);
                var role = await _roleRepository.GetRoleByNameAsync(RoleEnum.MEMBER) ?? throw new Exception(ServerErrorEnum.SERVER_ERROR);
                if (accLoggedId.RoleId != role.Id)
                {
                    throw new Exception(ServerErrorEnum.NOT_AUTHORIZED);
                }

                var posts = (await _postRepository.GetListPostAsync()).Where(p => p.DeleteDateTime == null).ToList();
                var listPostResDto = new List<GetPostResDto>();
                foreach (var post in posts)
                {
                    var postArtwork = await _postArtworkRepository.GetListPostArtworkByPostIdAsync(post.Id);
                    var creatorPost = await _accountRepository.GetAccountByIdAsync(post.CreatorId) ?? new Account();
                    var likePost = await _likeByRepository.GetListLikeByPostIdAsync(post.Id);
                    var listArtwork = new List<GetArtworkResDto>();
                    foreach (var artwork in postArtwork)
                    {
                        var art = await _artworkRepository.GetArtworkByIdAsync(artwork.ArtworkId) ?? new Artwork();
                        var creator = await _accountRepository.GetAccountByIdAsync(art.CreatorId) ?? new Account();
                        var likeArtwork = await _likeByRepository.GetListLikeByArtworkIdAsync(art.Id);
                        var listTypeOfArtwork = (await _artworkTypeRepository.GetAllArtworkTypeByArtworkIdAsync(art.Id)).Where(type => type.DeleteDateTime == null).ToList();
                        var typeOfArtworkResDto = new List<GetTypeOfArtworkResDto>();
                        foreach (var type in listTypeOfArtwork)
                        {
                            var typeOfArtwork = await _typeOfArtworkRepository.GetTypeOfArtworkByIdAsync(type.TypeOfArtworkId) ?? new TypeOfArtwork();
                            var typeArt = new GetTypeOfArtworkResDto
                            {
                                Id = type.TypeOfArtworkId,
                                Type = typeOfArtwork.Type,
                                TypeDescription = typeOfArtwork.TypeDescription
                            };
                            typeOfArtworkResDto.Add(typeArt);
                        }

                        var artworkResDto = new GetArtworkResDto
                        {
                            ArtworkId = art.Id,
                            ArtworkName = art.Name,
                            CreateDateTime = art.CreateDateTime,
                            CreatorName = creator.FirstName + " " + creator.LastName,
                            Image = art.Image,
                            TypeOfArtwork = typeOfArtworkResDto,
                            LikeCount = likeArtwork.Count,
                        };
                        listArtwork.Add(artworkResDto);
                    }
                    var accountLoggedIn = await _accountRepository.GetAccountByIdAsync(_helperService.GetAccIdFromLoogedNotThrow());
                    var postResDto = new GetPostResDto
                    {
                        PostId = post.Id,
                        ContentPost = post.ContentPost,
                        CreateDateTime = post.CreateDateTime,
                        CreatorName = creatorPost.FirstName + " " + creatorPost.LastName,
                        LikeCount = likePost.Count,
                        ListArtwork = listArtwork,
                        IsLike = accountLoggedIn != null ? likePost.Any(l => l.AccountId == accountLoggedIn.Id) : false
                    };
                    listPostResDto.Add(postResDto);
                }
                return listPostResDto;
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        public async Task<List<GetPostResDto>> GetListPostByCreatorNameByCustomerAsync(string creatorName)
        {
            try
            {

                if (!_helperService.IsTokenValid())
                {
                    throw new Exception(ServerErrorEnum.NOT_AUTHENTICATED);
                }
                var accLoggedId = await _accountRepository.GetAccountByIdAsync(_helperService.GetAccIdFromLogged()) ?? throw new Exception(ServerErrorEnum.NOT_AUTHENTICATED);
                var role = await _roleRepository.GetRoleByNameAsync(RoleEnum.MEMBER) ?? throw new Exception(ServerErrorEnum.SERVER_ERROR);
                if (accLoggedId.RoleId != role.Id)
                {
                    throw new Exception(ServerErrorEnum.NOT_AUTHORIZED);
                }
                if(String.IsNullOrEmpty(creatorName))
                {
                    throw new Exception("Creator name should not be empty!");
                }
                var posts = (await _postRepository.GetListPostByCreatorNameAsync(creatorName)).Where(p => p.DeleteDateTime == null).ToList();
                var listPostResDto = new List<GetPostResDto>();
                foreach (var post in posts)
                {
                    var postArtwork = await _postArtworkRepository.GetListPostArtworkByPostIdAsync(post.Id);
                    var creatorPost = await _accountRepository.GetAccountByIdAsync(post.CreatorId) ?? new Account();
                    var likePost = await _likeByRepository.GetListLikeByPostIdAsync(post.Id);
                    var listArtwork = new List<GetArtworkResDto>();
                    foreach (var artwork in postArtwork)
                    {
                        var art = await _artworkRepository.GetArtworkByIdAsync(artwork.ArtworkId) ?? new Artwork();
                        var creator = await _accountRepository.GetAccountByIdAsync(art.CreatorId) ?? new Account();
                        var likeArtwork = await _likeByRepository.GetListLikeByArtworkIdAsync(art.Id);
                        var listTypeOfArtwork = (await _artworkTypeRepository.GetAllArtworkTypeByArtworkIdAsync(art.Id)).Where(type => type.DeleteDateTime == null).ToList();
                        var typeOfArtworkResDto = new List<GetTypeOfArtworkResDto>();
                        foreach (var type in listTypeOfArtwork)
                        {
                            var typeOfArtwork = await _typeOfArtworkRepository.GetTypeOfArtworkByIdAsync(type.TypeOfArtworkId) ?? new TypeOfArtwork();
                            var typeArt = new GetTypeOfArtworkResDto
                            {
                                Id = type.TypeOfArtworkId,
                                Type = typeOfArtwork.Type,
                                TypeDescription = typeOfArtwork.TypeDescription
                            };
                            typeOfArtworkResDto.Add(typeArt);
                        }

                        var artworkResDto = new GetArtworkResDto
                        {
                            ArtworkId = art.Id,
                            ArtworkName = art.Name,
                            CreateDateTime = art.CreateDateTime,
                            CreatorName = creator.FirstName + " " + creator.LastName,
                            Image = art.Image,
                            TypeOfArtwork = typeOfArtworkResDto,
                            LikeCount = likeArtwork.Count,
                        };
                        listArtwork.Add(artworkResDto);
                    }
                    var accountLoggedIn = await _accountRepository.GetAccountByIdAsync(_helperService.GetAccIdFromLoogedNotThrow());
                    var postResDto = new GetPostResDto
                    {
                        PostId = post.Id,
                        ContentPost = post.ContentPost,
                        CreateDateTime = post.CreateDateTime,
                        CreatorName = creatorPost.FirstName + " " + creatorPost.LastName,
                        LikeCount = likePost.Count,
                        ListArtwork = listArtwork,
                        IsLike = accountLoggedIn != null ? likePost.Any(l => l.AccountId == accountLoggedIn.Id) : false
                    };
                    listPostResDto.Add(postResDto);
                }
                return listPostResDto;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<GetPostResDto> GetPostByIdAsync(Guid postId)
        {
            try
            {
                var post = await _postRepository.GetPostByIdAsync(postId) ?? throw new Exception("POST_NOT_FOUND");
                var postArtwork = await _postArtworkRepository.GetListPostArtworkByPostIdAsync(post.Id);
                var creatorPost = await _accountRepository.GetAccountByIdAsync(post.CreatorId) ?? new Account();
                var likePost = await _likeByRepository.GetListLikeByPostIdAsync(post.Id);
                var listArtwork = new List<GetArtworkResDto>();
                foreach (var artwork in postArtwork)
                {
                    var art = await _artworkRepository.GetArtworkByIdAsync(artwork.ArtworkId) ?? new Artwork();
                    var creator = await _accountRepository.GetAccountByIdAsync(art.CreatorId) ?? new Account();
                    var likeArtwork = await _likeByRepository.GetListLikeByArtworkIdAsync(art.Id);
                    var listTypeOfArtwork = (await _artworkTypeRepository.GetAllArtworkTypeByArtworkIdAsync(art.Id)).Where(type => type.DeleteDateTime == null).ToList();
                    var typeOfArtworkResDto = new List<GetTypeOfArtworkResDto>();
                    foreach (var type in listTypeOfArtwork)
                    {
                        var typeOfArtwork = await _typeOfArtworkRepository.GetTypeOfArtworkByIdAsync(type.TypeOfArtworkId) ?? new TypeOfArtwork();
                        var typeArt = new GetTypeOfArtworkResDto
                        {
                            Id = type.TypeOfArtworkId,
                            Type = typeOfArtwork.Type,
                            TypeDescription = typeOfArtwork.TypeDescription                                
                        };
                        typeOfArtworkResDto.Add(typeArt);
                    }
                    
                    var artworkResDto = new GetArtworkResDto
                    {
                        ArtworkId = art.Id,
                        ArtworkName = art.Name,
                        CreateDateTime = art.CreateDateTime,
                        CreatorName = creator.FirstName + " " + creator.LastName,
                        Image = art.Image,
                        TypeOfArtwork = typeOfArtworkResDto,
                        LikeCount = likeArtwork.Count,
                    };
                    listArtwork.Add(artworkResDto);
                }
                var postResDto = new GetPostResDto
                {
                    PostId = post.Id,
                    ContentPost = post.ContentPost,
                    CreateDateTime = post.CreateDateTime,
                    CreatorName = creatorPost.FirstName + " " + creatorPost.LastName,
                    LikeCount = likePost.Count,
                    ListArtwork = listArtwork
                };
                return postResDto;
            } catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<GetPostResDto>> GetListPostByCreatorIdAsync(Guid creatorId)
        {
            try
            {                
                var creator = await _accountRepository.GetAccountByIdAsync(creatorId) ?? throw new Exception("CREATOR_NOT_FOUND");
                var posts = await _postRepository.GetListPostByCreatorIdAsync(creator.Id);
                var listPostResDto = new List<GetPostResDto>();
                foreach(var post in posts)
                {
                    var postArtwork = await _postArtworkRepository.GetListPostArtworkByPostIdAsync(post.Id);
                    var listArtworkInPost = new List<GetArtworkResDto>();
                    foreach(var artwork in postArtwork)
                    {
                        var art = await _artworkRepository.GetArtworkByIdAsync(artwork.ArtworkId) ?? new Artwork();                        
                        var likeArtwork = await _likeByRepository.GetListLikeByArtworkIdAsync(art.Id);              
                        var artworkResDto = new GetArtworkResDto
                        {
                            ArtworkId = art.Id,
                            ArtworkName = art.Name,                    
                            Image = art.Image                           
                        };
                        listArtworkInPost.Add(artworkResDto);
                    }
                    var likePost = await _likeByRepository.GetListLikeByPostIdAsync(post.Id);
                    var accountLogIn = await _accountRepository.GetAccountByIdAsync(_helperService.GetAccIdFromLoogedNotThrow());
                    var postResDto = new GetPostResDto
                    {
                        PostId = post.Id,
                        ContentPost = post.ContentPost,
                        CreateDateTime = post.CreateDateTime,
                        CreatorName = creator.FirstName + " " + creator.LastName,
                        LikeCount = likePost.Count,
                        ListArtwork = listArtworkInPost,
                        IsLike = accountLogIn != null ? likePost.Any(l => l.AccountId == accountLogIn.Id) : false                        
                    };
                    listPostResDto.Add(postResDto);
                }
                return listPostResDto;
            } catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> UpdatePostAsync(PostRequestDto postRequest)
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
                var creator = await _accountRepository.GetAccountByIdAsync(postRequest.CreatorId) ?? throw new Exception("CREATOR_NOT_FOUND");
                var posts = await _postRepository.GetPostByIdAsync(postRequest.PostId) ?? throw new Exception("POST_NOT_FOUND");
                posts.ContentPost = postRequest.ContentPost;
                _postRepository.UpdatePostAsync(posts);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
