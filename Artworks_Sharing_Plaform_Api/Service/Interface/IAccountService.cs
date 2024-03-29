using Artworks_Sharing_Plaform_Api.Model.Dto.ReqDto;
using Artworks_Sharing_Plaform_Api.Model.Dto.ResDto;

namespace Artworks_Sharing_Plaform_Api.Service.Interface
{
    public interface IAccountService
    {
        Task<string> LoginAsync(LoginReqDto acc);
        Task<bool> CreateAccountMemberAsync(CreateAccountReqDto acc);
        Task<bool> CreateAccountCreatorAsync(CreateAccountReqDto acc);
        Task<bool> CreateAccountAdminAsync(CreateAccountReqDto acc);
        Task<ICollection<GetAccountResponseDto>?> GetListAccountAsync();
        Task<bool> UpdateProfileAccountAsync(UpdateProfileReqDto account);
        Task<string> GetRoleAccountLoggedInAsync();
        Task<List<GetCreatorResDto>> GetListCreatorAsync();
        Task<GetLoggedAccountResDto> GetLoggedAccountAsync();
        Task<List<GetAccountResponseDto>> GetListAccountByRoleAdminAsync(string roleName);
        Task<bool> DeleteAccountByRoleAdminAsync(Guid id, string roleName);
        Task<bool> ActiveAccountAsync(Guid id);
        Task<bool> UpdateAccountbyRoleAdminAsync(UpdateAccountReqDto dto);
        Task<bool> ChangeAccountStatusByRoleAdminAsync(ChangeStatusRequestDto requestDto);
        Task<string> ChangePasswordAuthenAsync(ChangePassowordAuthenDTO passowordDTO);
        Task<string> ChangePasswordNotAuthenAsync(ChangePasswordNotAuthenDTO passowordDTO);
        Task<string> GetOPTChangePasswordAsync(string Email);
        Task<bool> AcceptRequestRegisterCreator(bool isAccept, Guid accountId);
        Task<List<GetRequestCreatorResDto>> GetListRequestCreatorAsync();
        Task<List<GetAccountResponseDto>> GetListCreatorWithActiveStatusAsync();
        Task<GetArtistByCustomerResDto> GetArtistByCustomerAsync(Guid artistId);
        Task<List<GetAccountResponseDto>> GetListAccountCreatorByRoleAdminAsync();
        Task<List<GetAccountResponseDto>> GetListCreatorRequestByRoleAdminAsync();
        Task<bool> DeleteCreatorRequestAccountByRoleAdminAsync(Guid id);
    }
}
