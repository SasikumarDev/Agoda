using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Agoda.Common;
using Agoda.Core.IConfiguration;
using Agoda.Dtos;
using Agoda.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Agoda.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SiteUserController : ControllerBase
{
    private readonly ILogger<SiteUserController> _logger;
    private readonly General _general;
    private readonly IUnitofWork _unitofWork;
    public SiteUserController(ILogger<SiteUserController> logger, General general, IUnitofWork unitofWork)
    {
        _logger = logger;
        _general = general;
        _unitofWork = unitofWork;
    }

    [HttpPost, AllowAnonymous]
    [Route("[action]")]
    public async Task<IActionResult> RegisterUser(SiteUserDto siteUserDto)
    {
        var exeUser = await _unitofWork.siteUserRepository.getOneByCondition(x => x.Email == siteUserDto.Email);
        if (exeUser == null)
        {
            var user = new SiteUser()
            {
                Email = siteUserDto.Email,
                Password = _general.HashPassword(siteUserDto.Password),
                Name = siteUserDto.Name
            };
            await _unitofWork.siteUserRepository.Add(user);
            await _unitofWork.SaveChanges();
            return Ok(user);
        }
        return BadRequest(new { Message = "Username already exists" });
    }

    [HttpPost, AllowAnonymous]
    [Route("[action]")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        loginDto.Password = _general.HashPassword(loginDto.Password);
        var user = await _unitofWork.siteUserRepository.getOneByCondition(x => x.Email == loginDto.Username && x.Password == loginDto.Password);
        if (user is null)
        {
            return BadRequest(new { Message = "Invalid User name or Password" });
        }
        var claims = new[] {
        new Claim(JwtRegisteredClaimNames.Sub,user.Sid.ToString()),
        new Claim("IsSiteUser",bool.FalseString),
        new Claim("Email",user.Email),
        new Claim("Name",user.Name)
       };
        var token = _general.generateJwtToken(claims);
        return Ok(new { token });
    }

    [HttpGet, Authorize]
    [Route("[action]")]
    public async Task<IActionResult> getAllUsers()
    {
        var data = await _unitofWork.siteUserRepository.getAll();
        return Ok(data);
    }
}
