using Artworks_Sharing_Plaform_Api.AppDatabaseContext;
using Artworks_Sharing_Plaform_Api.Model;
using Artworks_Sharing_Plaform_Api.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Artworks_Sharing_Plaform_Api.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly ArtworksSharingPlaformDatabaseContext _db;
        public AccountRepository(ArtworksSharingPlaformDatabaseContext db)
        {
            _db = db;
        }

        public async Task<bool> CreateAccountAsync(Account account)
        {
            try
            {
                await _db.Accounts.AddAsync(account);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Get All Account
        public async Task<List<Account>?> GetAllAccountAsync()
        {
            try
            {
                return await _db.Accounts.ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Get Account By Id
        public async Task<Account?> GetAccountByIdAsync(Guid id)
        {
            try
            {
                return await _db.Accounts.FindAsync(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Update Account
        public async Task<bool> UpdateAccountAsync(Account account)
        {
            try
            {
                _db.Accounts.Update(account);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Account?> GetAccountByEmailAsync(string email)
        {
            try
            {
                return await _db.Accounts.FirstOrDefaultAsync(x => x.Email == email);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ICollection<Account>> GetListAccountAsync()
        {
            try
            {
                return await _db.Accounts
                    .Include(acc => acc.Role)
                    .Include(acc => acc.Status)
                    .ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Account>?> GetAllAccountByRoleIdAsync(Guid roleId)
        {
            try
            {
                return await _db.Accounts
                    .Include(acc => acc.Role)
                    .Include(acc => acc.Status)
                    .Where(acc => acc.RoleId == roleId)
                    .ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Account>?> GetAllAccountStatusByRoleIdAsync(Guid roleId, Guid statusId)
        {
            try
            {
                return await _db.Accounts
                    .Where(acc => acc.RoleId == roleId && acc.StatusId == statusId)
                    .Include(acc => acc.Role)
                    .Include(acc => acc.Status)
                    .ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Account>?> GetAllAccountNotIncludeStatusByRoleIdAsync(Guid roleId, Guid statusId)
        {
            try
            {
                return await _db.Accounts
                    .Where(acc => acc.RoleId == roleId && acc.StatusId != statusId)
                    .Include(acc => acc.Role)
                    .Include(acc => acc.Status)
                    .ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> RemoveAccountAsync(Account account)
        {
            try
            {
                _db.Accounts.Remove(account);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public IQueryable<Account> GetAll()
        {
            return _db.Set<Account>();
        }
    }
}
