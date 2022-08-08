using Agoda.Core.IRepository;

namespace Agoda.Core.IConfiguration;

public interface IUnitofWork
{
    ISiteUserRepository siteUserRepository { get; }
    Task SaveChanges();
}