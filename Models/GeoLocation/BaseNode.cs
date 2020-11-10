using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace FindIt.API.Models.GeoLocation
{
    public class BaseNode
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("name")]
        [MaxLength(20,ErrorMessage ="Location name can not be longer than 20")]
        public string Name { get; set; }

        [BsonElement("tag")]
        [MaxLength(30, ErrorMessage = "Location tag can not be longer than 30")]
        public string Tag { get; set; }

        [BsonElement("description")]
        [MaxLength(1000, ErrorMessage = "Location description can not be longer than 100")]
        public string Description { get; set; }
    }
}
