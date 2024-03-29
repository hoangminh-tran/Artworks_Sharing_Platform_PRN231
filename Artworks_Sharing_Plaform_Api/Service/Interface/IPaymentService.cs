namespace Artworks_Sharing_Plaform_Api.Service.Interface
{
    public interface IPaymentService
    {
        Task<string> CreateZaloPayRequest(decimal amount);
        Task<bool> CallbackZaloPay(string app_trans_id);
    }
}
