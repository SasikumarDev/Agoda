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
        var result = (from x in data.ToList()
                      select new
                      {
                          Id = x.Id,
                          RequestDateTime = x.RequestDateTime.ToString("dd-MMM-yyyy hh:mm:ss tt"),
                          RequestPath = x.RequestPath,
                          RequestType = x.RequestType,
                          ResponseDateTime = x.ResponseDateTime.ToString("dd-MMM-yyyy hh:mm:ss tt"),
                          ResponseStatus = x.ResponseStatus
                      }
         ).OrderByDescending(x => x.Id).ToList();
        return Ok(result);
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