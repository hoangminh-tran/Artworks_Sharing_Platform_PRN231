using Artworks_Sharing_Plaform_Api.Model.Dto.ReqDto;
using Artworks_Sharing_Plaform_Api.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Artworks_Sharing_Plaform_Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class StatisticsController : ControllerBase
{
    private readonly IStatisticsService _statisticsService;

    public StatisticsController(IStatisticsService statisticsService)
    {
        _statisticsService = statisticsService;
    }

    [HttpGet("account/year")]
    public async Task<ActionResult<Dictionary<int, StatisticsAccountDTO>>> GetStatisticsAccount(int year)
    {
        return Ok(await _statisticsService.GetStaticAccount(year));
    }
    [HttpGet("money/year")]
    public async Task<ActionResult<Dictionary<int, StatisticsAccountDTO>>> GetStatisticsMoney(int year)
    {
        return Ok(await _statisticsService.GetTotalMoneyInMonth(year));
    }
    [HttpGet("TotalStatistics")]
    public async Task<IActionResult> GetTotalStatistics()
    {
        return Ok(await _statisticsService.GetTotalStatistics());
    }
}
