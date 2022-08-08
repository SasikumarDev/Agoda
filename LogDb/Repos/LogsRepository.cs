using Agoda.Models;
using MongoDB.Driver;

namespace Agoda.LogDb.Repos;

public class LogsRepository : ILogsRepository
{
    private readonly ILogDbContext _logDbContext;
    public LogsRepository(ILogDbContext logDbContext)
    {
        _logDbContext = logDbContext;
    }
    public async Task Add(Logs logs)
    {
        await _logDbContext.Logs.InsertOneAsync(logs);
    }

    public async Task<IEnumerable<Logs>> getAll()
    {
        var d = await _logDbContext.Logs.FindAsync(x => true);
        var result = await d.ToListAsync();
        return result;
    }
}