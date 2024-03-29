using Artworks_Sharing_Plaform_Api.AppDatabaseContext;
using Artworks_Sharing_Plaform_Api.Model;
using Artworks_Sharing_Plaform_Api.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Artworks_Sharing_Plaform_Api.Repository
{
    public class PaymentHistoryRepository : IPaymentHistoryRepository
    {
        private readonly ArtworksSharingPlaformDatabaseContext _context;

        public PaymentHistoryRepository(ArtworksSharingPlaformDatabaseContext context)
        {
            _context = context;
        }

        public async Task<bool> CreatePaymentHistoryAsync(PaymentHistory paymentHistory)
        {
            try
            {
                await _context.PaymentHistories.AddAsync(paymentHistory);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<PaymentHistory?> GetPaymentHistoryByCodeAsync(string code)
        {
            try
            {
                return await _context.PaymentHistories.FirstOrDefaultAsync(x => x.Code == code);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> UpdatePaymentHistoryAsync(PaymentHistory paymentHistory)
        {
            try
            {
                _context.PaymentHistories.Update(paymentHistory);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
