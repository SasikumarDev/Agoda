namespace Agoda.LogDb.Repos;

public interface ILogsRepository
{
    Task Add(Models.Logs logs);
    Task<IEnumerable<Models.Logs>> getAll();
}