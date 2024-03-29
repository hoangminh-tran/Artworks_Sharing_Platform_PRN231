using Artworks_Sharing_Plaform_Api.Model;
using Artworks_Sharing_Plaform_Api.Repository.Interface;
using Artworks_Sharing_Plaform_Api.Service.Interface;
using Newtonsoft.Json;
using ZaloPay.Helper;
using ZaloPay.Helper.Crypto;

namespace Artworks_Sharing_Plaform_Api.Service
{
    public class PaymentService : IPaymentService
    {
        private readonly IHelpperService _helperService;
        private readonly IPaymentHistoryRepository _paymentHistoryRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IStatusRepository _statusRepository;

        public PaymentService(IHelpperService helperService, IPaymentHistoryRepository paymentHistoryRepository, IAccountRepository accountRepository, IStatusRepository statusRepository)
        {
            _helperService = helperService;
            _paymentHistoryRepository = paymentHistoryRepository;
            _accountRepository = accountRepository;
            _statusRepository = statusRepository;
        }

        public async Task<string> CreateZaloPayRequest(decimal amount)
        {
            try
            {
                if (!_helperService.IsTokenValid())
                {
                    throw new Exception("Token is invalid");
                }
                var customer = await _accountRepository.GetAccountByIdAsync(_helperService.GetAccIdFromLogged());
                if (customer == null)
                {
                    throw new Exception("Customer not found");
                }
                string app_id = "2553";
                string key1 = "PcY4iZIKFCIdgZvA6ueMcMHHUbRLYjPL";
                string create_order_url = "https://sb-openapi.zalopay.vn/v2/create";

                Random rnd = new Random();
                var embed_data = new
                {
                    redirecturl = "https://localhost:7023/callback",
                    accountId = customer.Id.ToString()
                };
                var items = new[] { new { } };
                var param = new Dictionary<string, string>();
                var app_trans = rnd.Next(1000000); // Generate a random order's ID.
                var app_trans_id = DateTime.Now.ToString("yyMMdd") + "_" + app_trans;
                param.Add("app_id", app_id);
                param.Add("app_user", "user123");
                param.Add("app_time", Utils.GetTimeStamp().ToString());
                param.Add("amount", amount.ToString());
                param.Add("app_trans_id", app_trans_id);
                param.Add("embed_data", JsonConvert.SerializeObject(embed_data));
                param.Add("item", JsonConvert.SerializeObject(items));
                param.Add("description", "RentHouse - Thanh toán đơn hàng #" + app_trans_id);
                param.Add("bank_code", "zalopayapp");
                param.Add("email", "khangbpak2001@gmail.com");


                var data = app_id + "|" + param["app_trans_id"] + "|" + param["app_user"] + "|" + param["amount"] + "|"
                    + param["app_time"] + "|" + param["embed_data"] + "|" + param["item"];
                param.Add("mac", HmacHelper.Compute(ZaloPayHMAC.HMACSHA256, key1, data));

                var response = await HttpHelper.PostFormAsync(create_order_url, param);
                // convert response to object

                string result = JsonConvert.SerializeObject(response);
                await _paymentHistoryRepository.CreatePaymentHistoryAsync(new PaymentHistory
                {
                    AccountId = customer.Id,
                    Amount = amount,
                    Code = app_trans_id,
                    StatusId = (await _statusRepository.GetStatusByNameAsync("PENDING")).Id
                });
                return result;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<bool> CallbackZaloPay(string app_trans_id)
        {
            try
            {
                var paymentHistory = await _paymentHistoryRepository.GetPaymentHistoryByCodeAsync(app_trans_id);
                if (paymentHistory == null)
                {
                    throw new Exception("Payment history not found");
                }
                var status = await _statusRepository.GetStatusByNameAsync("ACCEPTED");
                paymentHistory.StatusId = status.Id;
                await _paymentHistoryRepository.UpdatePaymentHistoryAsync(paymentHistory);
                var account = await _accountRepository.GetAccountByIdAsync(paymentHistory.AccountId) ?? throw new Exception("Account not found");
                account.Balance += paymentHistory.Amount;
                await _accountRepository.UpdateAccountAsync(account);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
