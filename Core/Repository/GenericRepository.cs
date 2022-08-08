using Agoda.Core.IRepository;
using Agoda.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Agoda.Core.Repository;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    protected DbSet<T> dbSet;
    protected AgodaDbContext _agodaDbContext;
    public GenericRepository(AgodaDbContext agodaDbContext)
    {
        _agodaDbContext = agodaDbContext;
        dbSet = _agodaDbContext.Set<T>();
    }
    public async Task Add(T data)
    {
        await dbSet.AddAsync(data);
    }

    public async Task<IEnumerable<T>> getAll()
    {
        var data = await dbSet.ToListAsync();
        return data;
    }

    public async Task<IEnumerable<T>> getByCondition(Expression<Func<T, bool>> predicate)
    {
        var data = await dbSet.Where(predicate).ToListAsync();
        return data;
    }

    public async Task<T> getOneByCondition(Expression<Func<T, bool>> predicate)
    {
        var data = await dbSet.FirstOrDefaultAsync(predicate);
        return data;
    }
}