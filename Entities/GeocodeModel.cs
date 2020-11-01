using FindIt.DataAccess.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver.GeoJsonObjectModel;

namespace FindIt.Backend.Entities
{
    [BsonIgnoreExtraElements]
    public class GeocodeModel:BaseEntity
    {
       
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
