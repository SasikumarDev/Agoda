using System.Security.Cryptography;
using System.Text;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace Agoda.Common;

public class General
{
    private readonly IConfiguration _configuration;
    public General(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public string HashPassword(string Password)
    {
        StringBuilder strB = new StringBuilder();
        var sha = SHA256.Create();
        byte[] bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(Password));
        foreach (byte b in bytes)
        {
            strB.Append(b.ToString("X2"));
        }
        return strB.ToString();
    }

    public object generateJwtToken(Claim[] claims)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtConfig:Key"]));
        var signCred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(_configuration["JwtConfig:Issuer"],
                _configuration["JwtConfig:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: signCred);
        string access_token = new JwtSecurityTokenHandler().WriteToken(token);
        return new { access_token };
    }
}