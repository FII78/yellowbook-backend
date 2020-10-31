using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver.GeoJsonObjectModel;
using System.Text.Json.Serialization;

namespace FindIt.Backend.Entities
{  
    [BsonIgnoreExtraElements]
    public class GeocodeModel
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






    }
   
}
