using Agoda.Core.IConfiguration;
using Agoda.LogDb.Repos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Agoda.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize("SiteUser")]
public class LogMonitorController : ControllerBase
{
    private readonly IMongoUOW _mongoUOW;
    private readonly IUnitofWork _unitofWork;
    public LogMonitorController(IMongoUOW mongoUOW, IUnitofWork unitofWork)
    {
        _mongoUOW = mongoUOW;
        _unitofWork = unitofWork;
    }

    [HttpGet]
    [Route("[action]")]
    public async Task<IActionResult> getAllLogs()
    {
        var data = await _mongoUOW.LogsRepository.getAll();
        return Ok(data);
    }
    [HttpGet]
    [Route("[action]")]
    public async Task<IActionResult> getHomeDetails()
    {
        var siteUsers = await _unitofWork.siteUserRepository.getAll();
        var details = new { Siteusers = siteUsers.Count() };
        return Ok(details);
    }
}