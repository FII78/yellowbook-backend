using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FindIt.API.Models.GeoLocation
{
    public class BaseNode
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("tag")]
        public string Tag { get; set; }

        [BsonElement("description")]
        public string Description { get; set; }
    }
}
