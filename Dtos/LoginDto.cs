using System.ComponentModel.DataAnnotations;

namespace Agoda.Dtos;

public class LoginDto
{
    [Required]
    [DataType(DataType.EmailAddress)]
    public string Username { get; set; }
    
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}