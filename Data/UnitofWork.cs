using Agoda.Core.IConfiguration;
using Agoda.Core.IRepository;
using Agoda.Core.Repository;

namespace Agoda.Data;

public class UnitofWork : IUnitofWork, IDisposable
{
    protected AgodaDbContext _agodaDbContext;
    public UnitofWork(AgodaDbContext agodaDbContext)
    {
        _agodaDbContext = agodaDbContext;
        siteUserRepository = new SiteUserRepository(_agodaDbContext);
    }

    public ISiteUserRepository siteUserRepository { get; private set; }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }

    public async Task SaveChanges()
    {
        await _agodaDbContext.SaveChangesAsync();
    }
}