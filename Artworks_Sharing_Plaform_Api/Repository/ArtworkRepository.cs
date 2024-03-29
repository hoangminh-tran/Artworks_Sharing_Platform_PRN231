using Artworks_Sharing_Plaform_Api.AppDatabaseContext;
using Artworks_Sharing_Plaform_Api.Enum;
using Artworks_Sharing_Plaform_Api.Model;
using Artworks_Sharing_Plaform_Api.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Artworks_Sharing_Plaform_Api.Repository
{
    public class ArtworkRepository : IArtworkRepository
    {
        private readonly ArtworksSharingPlaformDatabaseContext _db;
        public ArtworkRepository(ArtworksSharingPlaformDatabaseContext db)
        {
            _db = db;
        }

        public async Task<Artwork> CreateArtworkAsync(Artwork artwork)
        {
            try
            {
                await _db.Artworks.AddAsync(artwork);
                await _db.SaveChangesAsync();
                return artwork;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Artwork?> GetArtworkByIdAsync(Guid artworkId)
        {
            try
            {
                return await _db.Artworks.FirstOrDefaultAsync(aw => aw.Id.Equals(artworkId));
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Artwork?> GetArtworkByArtworkByIdAsync(Guid artworkId)
        {
            try
            {
                return await _db.Artworks.FindAsync(artworkId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Artwork>> GetListArtworkByCreatorIdAsync(Guid creatorId)
        {
            try
            {
                return await _db.Artworks
                    .Where(aw => aw.CreatorId.Equals(creatorId))
                    .Include(artwork => artwork.Creator)
                    .Include(artwork => artwork.Status)
                    .OrderByDescending(artwork => artwork.CreateDateTime)
                    .ToListAsync();
            } catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Artwork>> GetListArtworkByUserIdAsync(Guid userId)
        {
            try
            {
                var query = from artwork in _db.Artworks
                            join order in _db.Orders on artwork.OrderId equals order.Id
                            where order.AccountId.Equals(userId)
                            select artwork;
                return await query
                    .Include(artwork => artwork.Creator)
                    .Include(artwork => artwork.Status)
                    .OrderByDescending(artwork => artwork.CreateDateTime)
                    .ToListAsync();
            } catch (Exception)
            {
                throw;
            }
        }

        public async Task<Artwork?> GetArtworkByOrderIdAsync(Guid orderId)
        {
            try
            {
                return await _db.Artworks.Where(p => p.OrderId.Equals(orderId)).FirstOrDefaultAsync();
            } catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> UpdateArtworkAsync(Artwork artwork)
        {
            try
            {
                _db.Artworks.Update(artwork);
                await _db.SaveChangesAsync();
                return true;
            } catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Artwork>> GetListArtworkOwnByUserAsync(Guid userId)
        {
            try
            {
                var query = from artwork in _db.Artworks
                            join order in _db.Orders on artwork.OrderId equals order.Id
                            where order.AccountId.Equals(userId)
                            select artwork;
                return await query
                    .Include(artwork => artwork.Creator)
                    .Include(artwork => artwork.Status)
                    .OrderByDescending(artwork => artwork.CreateDateTime)
                    .ToListAsync();
            } catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Artwork>> GetListArtworkAsync()
        {
            try
            {
                return await _db.Artworks
                    .Include(artwork => artwork.Creator)
                    .Include(artwork => artwork.Status)
                    .OrderByDescending(artwork => artwork.CreateDateTime)
                    .ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<List<Artwork>> GetListArtworkByArtworkNameAsync(string? artworkName, Guid statusId)
        {
            try
            {
                if (artworkName == null)
                {
                    return await _db.Artworks
                        .Where(aw => aw.StatusId == statusId && aw.DeleteDateTime == null)
                        .Include(artwork => artwork.Creator)
                        .Include(artwork => artwork.Status)
                        .OrderByDescending(artwork => artwork.CreateDateTime)
                        .ToListAsync();
                }
                else
                {
                    return await _db.Artworks
                        .Where(aw => aw.Name.Contains(artworkName) && aw.StatusId == statusId && aw.DeleteDateTime == null)
                        .Include(artwork => artwork.Creator)
                        .Include(artwork => artwork.Status)
                        .OrderByDescending(artwork => artwork.CreateDateTime)
                        .ToListAsync();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Artwork>> GetListArtworkByCreatorIdAndStatusAsync(Guid creatorId, Guid statusId)
        {
            try
            {
                return await _db.Artworks
                    .Where(aw => aw.CreatorId == creatorId && aw.StatusId == statusId && aw.DeleteDateTime == null)
                    .Include(artwork => artwork.Creator)
                    .Include(artwork => artwork.Status)
                    .OrderByDescending(artwork => artwork.CreateDateTime)
                    .ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Artwork>> GetListArtworkByFilterListTypeOfArtworkAndArtistAsync(List<Guid>? typeOfArtworkIds, Guid? artistId, Guid statusId)
        {
            try
            {
                var query = _db.Artworks.AsQueryable();

                if (typeOfArtworkIds != null)
                {
                    query = query.Join(_db.ArtworkTypes,
                                       artwork => artwork.Id,
                                       artworkType => artworkType.ArtworkId,
                                       (artwork, artworkType) => new { Artwork = artwork, ArtworkType = artworkType })
                                 .Where(x => typeOfArtworkIds.Contains(x.ArtworkType.TypeOfArtworkId))
                                 .Select(x => x.Artwork);
                }

                if (artistId != null)
                {
                    query = query.Where(aw => aw.CreatorId == artistId);
                }

                query = query.Where(aw => aw.StatusId == statusId && aw.DeleteDateTime == null)
                             .Include(artwork => artwork.Creator)
                             .Include(artwork => artwork.Status)
                             .OrderByDescending(artwork => artwork.CreateDateTime);
                return await query.ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
