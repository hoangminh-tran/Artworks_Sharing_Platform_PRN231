using Artworks_Sharing_Plaform_Api.Model;

namespace Artworks_Sharing_Plaform_Api.Repository.Interface
{
    public interface IAccountRepository
    {
        Task<bool> CreateAccountAsync(Account acc);
        Task<List<Account>?> GetAllAccountAsync();
        Task<Account?> GetAccountByIdAsync(Guid id);
        Task<bool> UpdateAccountAsync(Account acc);
        Task<Account?> GetAccountByEmailAsync(string email);
        Task<ICollection<Account>> GetListAccountAsync();
        Task<List<Account>?> GetAllAccountByRoleIdAsync(Guid roleId);
        Task<List<Account>?> GetAllAccountStatusByRoleIdAsync(Guid roleId, Guid statusId);
        Task<List<Account>?> GetAllAccountNotIncludeStatusByRoleIdAsync(Guid roleId, Guid statusId);
        Task<bool> RemoveAccountAsync(Account acc);
        public IQueryable<Account> GetAll();
    }
}
