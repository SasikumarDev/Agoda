namespace Agoda.Models;

public class SiteUser
{
    public Guid Sid { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public bool Status { get; set; }
}