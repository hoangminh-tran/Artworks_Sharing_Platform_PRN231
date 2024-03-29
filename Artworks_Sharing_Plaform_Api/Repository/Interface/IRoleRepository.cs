using Artworks_Sharing_Plaform_Api.Model;

namespace Artworks_Sharing_Plaform_Api.Repository.Interface
{
    public interface IRoleRepository
    {
        Task<Role?> GetRoleByNameAsync(string name);
        Task<Role?> GetRoleByRoleIDAsync(Guid ID);
    }
}
