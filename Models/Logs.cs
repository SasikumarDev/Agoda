using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Agoda.Models;

public class Logs
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string RequestType { get; set; }
    public string RequestPath { get; set; }
    public string RequestBody { get; set; }
    public DateTime RequestDateTime { get; set; }
    public string ResponseBody { get; set; }
    public string ResponseStatus { get; set; }
    public DateTime ResponseDateTime { get; set; }
}