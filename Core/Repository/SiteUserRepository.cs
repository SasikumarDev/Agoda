using Agoda.Core.IRepository;
using Agoda.Data;
using Agoda.Models;

namespace Agoda.Core.Repository;

public class SiteUserRepository : GenericRepository<SiteUser>, ISiteUserRepository
{
    public SiteUserRepository(AgodaDbContext agodaDbContext) : base(agodaDbContext)
    {
    }
}