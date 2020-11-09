using FindIt.API.Models.GeoLocation;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace FindIt.Backend.Models
{
    public class NodeVM : BaseNode
    {


        [BsonElement("location")]
        [Required(ErrorMessage ="Location's longtude and Latitude is required")]
        public double[] Location { get; set; }

      
    }
}
