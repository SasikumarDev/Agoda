namespace Agoda.Models;


public class Hotel
{
    public Guid HID {get;set;}
    public string Name {get;set;}
    public bool isVerified {get;set;}
    public DateTime CreatedOn {get;set;}
    public DateTime? UpdatedOn {get;set;}
}