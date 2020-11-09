using FindIt.API.Models.GeoLocation;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FindIt.Backend.Models
{
    public class NodeVM:BaseNode
    {
        

        [BsonElement("location")]
        public double[] Location { get; set; }

      
    }
}
