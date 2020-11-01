using FindIt.API.Models.GeoLocation;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver.GeoJsonObjectModel;

namespace FindIt.Backend.Models
{
    public class NodeLoc:BaseNode
    {
      

        [BsonElement("location")]
        public GeoJsonPoint<GeoJson2DCoordinates> Location { get; set; }


    }
}
