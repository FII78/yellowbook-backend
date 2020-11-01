using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver.GeoJsonObjectModel;

namespace FindIt.Backend.Models
{
    public class NodeBase
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("location")]
        public GeoJsonPoint<GeoJson2DCoordinates> Location { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("tag")]
        public string Tag { get; set; }

        [BsonElement("description")]
        public string Description { get; set; }

    }
}
