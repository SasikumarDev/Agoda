using Agoda.Common;
using Agoda.Core.IConfiguration;
using Agoda.Data;
using Agoda.LogDb;
using Agoda.LogDb.Repos;

namespace Agoda.Extensions;

public static class DIExtensions
{
    public static IServiceCollection DIConfig(this IServiceCollection services)
    {
        services.AddScoped<General>();
        // Mongo Db
        services.AddScoped<ILogDbContext, LogDbContext>();
        services.AddScoped<IMongoUOW, MongoUOW>();
        // SQL Unit of work
        services.AddScoped<IUnitofWork, UnitofWork>();
        return services;
    }
}