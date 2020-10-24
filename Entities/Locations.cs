using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindIt.Backend.Entities
{
    public class Locations
    {
        /// <summary>
        /// Internal Id of location item
        /// </summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        /// <summary>
        /// Name of the location
        /// </summary>
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// An optional description of the location
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// The latitude (vertical) position of the location
        /// </summary>
        [Required]
        [Range(-90, 90)]
        public double Latitude { get; set; }
        /// <summary>
        /// The longitude (horizontal) position of the location
        /// </summary>
        [Required]
        [Range(-180, 180)]
        public double Longitude { get; set; }
    }
}
