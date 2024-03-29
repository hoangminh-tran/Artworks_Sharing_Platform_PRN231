using Artworks_Sharing_Plaform_Api.Model;
using Artworks_Sharing_Plaform_Api.Repository.Interface;
using Artworks_Sharing_Plaform_Api.Service.Interface;

namespace Artworks_Sharing_Plaform_Api.Service
{
    public class StatusService : IStatusService
    {
        private readonly IStatusRepository _statusRepository;
        public StatusService(IStatusRepository statusRepository)
        {
            _statusRepository = statusRepository;
        }

        public async Task<Status?> GetStatusByStatusName(string statusName)
        {
            try
            {
                return await _statusRepository.GetStatusByNameAsync(statusName);                
            }catch (Exception)
            {
                throw;
            }
        }
    }
}
