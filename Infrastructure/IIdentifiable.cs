using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SuvillianceSystem.Connections.Infrastructure
{
    public interface IIdentifiable
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
    }
}