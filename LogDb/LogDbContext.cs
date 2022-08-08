using Agoda.LogDb.Repos;
using MongoDB.Driver;
namespace Agoda.LogDb;

public class LogDbContext : ILogDbContext
{
    public LogDbContext(IConfiguration configuration)
    {
        var client = new MongoClient(configuration["LogdbConstr"]);
        var db = client.GetDatabase("AgodaLogs");
        Logs = db.GetCollection<Models.Logs>("Logs");
    }

    public IMongoCollection<Models.Logs> Logs { get; private set; }
}