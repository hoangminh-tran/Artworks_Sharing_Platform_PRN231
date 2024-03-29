using Artworks_Sharing_Plaform_Api.AppDatabaseContext;
using Artworks_Sharing_Plaform_Api.Model;
using Artworks_Sharing_Plaform_Api.Model.Dto.ReqDto;
using Artworks_Sharing_Plaform_Api.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Artworks_Sharing_Plaform_Api.Repository
{
    public class UserFollowerRepository : IUserFollowerRepository
    {
        private readonly ArtworksSharingPlaformDatabaseContext _db;
        public UserFollowerRepository(ArtworksSharingPlaformDatabaseContext db)
        {
            _db = db;
        }
        public async Task<bool> FollowAsync(UserFollow follow)
        {
            try
            {
                await _db.UserFollowers.AddAsync(follow);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> UnfollowAsync(UserFollow follow)
        {
            try
            {
                _db.UserFollowers.Remove(follow);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
