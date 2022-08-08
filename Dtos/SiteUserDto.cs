using System.ComponentModel.DataAnnotations;

namespace Agoda.Dtos;

public class SiteUserDto
{
    [Required]
    [Display(Name="Name")]
    public string Name { get; set; }

    [Required]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}