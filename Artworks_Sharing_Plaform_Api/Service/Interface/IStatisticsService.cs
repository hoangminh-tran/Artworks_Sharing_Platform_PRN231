using Artworks_Sharing_Plaform_Api.Model.Dto.ReqDto;
using Artworks_Sharing_Plaform_Api.Model.Dto.ResDto;

namespace Artworks_Sharing_Plaform_Api.Service.Interface;

public interface IStatisticsService
{
    public Task<Dictionary<int, StatisticsAccountDTO>> GetStaticAccount(int year);
    public Task<StatisticsTotalDto> GetTotalStatistics();

    // Done
    public Task<Dictionary<int, GetMoneyInMonth>> GetTotalMoneyInMonth(int year);
    //public Task<Dictionary<int, IList<GetMoneyInMonth>>> GetTotalMoneyInMonth(int year);

}
