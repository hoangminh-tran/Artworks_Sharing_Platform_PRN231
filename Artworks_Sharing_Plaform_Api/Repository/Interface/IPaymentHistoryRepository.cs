using Artworks_Sharing_Plaform_Api.Model;

namespace Artworks_Sharing_Plaform_Api.Repository.Interface
{
    public interface IPaymentHistoryRepository
    {
        Task<bool> CreatePaymentHistoryAsync(PaymentHistory paymentHistory);
        Task<PaymentHistory?> GetPaymentHistoryByCodeAsync(string code);
        Task<bool> UpdatePaymentHistoryAsync(PaymentHistory paymentHistory);
    }
}
