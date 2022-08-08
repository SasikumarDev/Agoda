using Agoda.LogDb.Repos;

namespace Agoda.LogDb;

public class MongoUOW : IMongoUOW,IDisposable
{
    public ILogsRepository LogsRepository { get; private set; }
    public MongoUOW(ILogDbContext logDbContext)
    {
        LogsRepository = new LogsRepository(logDbContext);
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}