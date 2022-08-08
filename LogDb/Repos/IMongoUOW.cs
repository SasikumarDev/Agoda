namespace Agoda.LogDb.Repos;

public interface IMongoUOW
{
    ILogsRepository LogsRepository { get; }
}