using Artworks_Sharing_Plaform_Api.Model;
using Artworks_Sharing_Plaform_Api.Model.Dto.ReqDto;
using Artworks_Sharing_Plaform_Api.Model.Dto.ResDto;
using Artworks_Sharing_Plaform_Api.Repository.Interface;
using Artworks_Sharing_Plaform_Api.Service.Interface;
using Microsoft.EntityFrameworkCore;

namespace Artworks_Sharing_Plaform_Api.Service;

public class StatisticsService : IStatisticsService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IArtworkRepository _artworkRepository;
    private readonly IAccountRepository _accountRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly Dictionary<int, IList<Order>> _listOrder = new();
    private readonly Dictionary<int, StatisticsAccountDTO> _listStatisticsAccountDTO = new();
    private readonly Dictionary<int, IList<Account>> _listAccount = new();
    private readonly Dictionary<int, IList<GetMoneyInMonth>> _listGetMoneyInMonth = new();
    public StatisticsService(IOrderRepository orderRepository,
        IArtworkRepository artworkRepository,
        IAccountRepository accountRepository,
        IRoleRepository roleRepository)
    {
        _orderRepository = orderRepository;
        _artworkRepository = artworkRepository;
        _accountRepository = accountRepository;
        _roleRepository = roleRepository;
        for (var i = 1; i <= 12; i++)
        {
            _listOrder.Add(i, new List<Order>());
            _listStatisticsAccountDTO.Add(i, new StatisticsAccountDTO());
            _listAccount.Add(i, new List<Account>());
            _listGetMoneyInMonth.Add(i, new List<GetMoneyInMonth>());
        }

    }

    // role /status

    // chua test
    public async Task<Dictionary<int, StatisticsAccountDTO>> GetStaticAccount(int year)
    {
        var account = await _accountRepository.GetAll().Where(_ => _.CreateDateTime.Year == year).ToListAsync();
        if (account == null)
        {
            throw new Exception($"Cant Not Found Any User In Year {year}");
        }
        account.ForEach(a =>
        {
            _listAccount[a.CreateDateTime.Month].Add(a);
        });
        for (var i = 1; i <= 12; i++)
        {
            foreach (var item in _listAccount[i])
            {
                var role = await _roleRepository.GetRoleByRoleIDAsync(item.RoleId);
                if (role != null)
                {
                    if (role.RoleName == "CREATOR")
                    {
                        // Error if CreatorRegisterInMonth not value
                        _listStatisticsAccountDTO[i].CreatorRegisterInMonth++;
                    }
                    else if (role.RoleName == "MEMBER")
                    {
                        _listStatisticsAccountDTO[i].MemberRegisterInMonth++;
                    }
                    else if (role.RoleName == "MODERATOR")
                    {
                        _listStatisticsAccountDTO[i].ModeratorRegisterInMonth++;
                    }
                }
            }
        }
        return _listStatisticsAccountDTO;
    }

    // Done
    public async Task<StatisticsTotalDto> GetTotalStatistics()
    {
        var staticTotal = new StatisticsTotalDto();
        staticTotal.TotalMoney = 0;

        List<Order> orders = await _orderRepository.GetAll().Where(_ => _.Status.StatusName == "PAID").ToListAsync();
        foreach (var item in orders)
        {
            var priceArtwork = await _artworkRepository.GetArtworkByOrderIdAsync(item.Id);
            if (priceArtwork != null)
            {
                staticTotal.TotalMoney += priceArtwork.Price;
            }
        }
        staticTotal.TotalArtworks = (await _artworkRepository.GetListArtworkAsync())
            .Count();
        staticTotal.TotalAccounts = (await _accountRepository.GetAll().Where(_ => _.Role.RoleName != "ADMIN").ToListAsync())
            .Count();
        return staticTotal;
    }

    // Done
    public async Task<Dictionary<int, GetMoneyInMonth>> GetTotalMoneyInMonth(int year)
    {
        var orders = await _orderRepository.GetAll().Where(_ => _.Status.StatusName == "PAID" && _.CreateDateTime.Year == year).ToListAsync();
        orders.ForEach(o =>
        {
            _listOrder[o.CreateDateTime.Month].Add(o);
        });
        var getMoneyInMonth = new Dictionary<int, GetMoneyInMonth>();
        for (var i = 1; i <= 12; i++)
        {
            decimal? moneyTotalMonth = 0;
            foreach (var item in _listOrder[i])
            {
                var priceArtwork = await _artworkRepository.GetArtworkByOrderIdAsync(item.Id);
                if (priceArtwork != null)
                {
                    moneyTotalMonth += priceArtwork.Price;
                }
            }
            getMoneyInMonth.Add(i, new GetMoneyInMonth
            {
                Month = i,
                Money = moneyTotalMonth
            });
        }

        return getMoneyInMonth;
    }
}
