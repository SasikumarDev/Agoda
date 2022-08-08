using MongoDB.Driver;
namespace Agoda.LogDb.Repos;

public interface ILogDbContext
{
    IMongoCollection<Models.Logs> Logs { get;}
}