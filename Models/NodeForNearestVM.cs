using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindIt.API.Models
{
    public class NodeForNearestVM
    {
        [BsonElement("latitude")]
        public double Latitude { get; set; }

        [BsonElement("longitude")]
        public double Longitude { get; set; }

        [BsonElement("tag")]
        public string Tag { get; set; }

        [BsonElement("radius")]
        public int Radius { get; set; }
    }
}
