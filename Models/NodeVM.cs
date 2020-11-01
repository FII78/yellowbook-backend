﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindIt.Backend.Models
{
    public class NodeVM
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("location")]
        public double[] Location { get; set; }
        [BsonElement("name")]
        public string Name { get; set; }
        [BsonElement("tag")]
        public string Tag { get; set; }
    }
}