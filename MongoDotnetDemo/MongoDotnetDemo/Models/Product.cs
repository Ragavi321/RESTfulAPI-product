﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace MongoDotnetDemo.Models
{
    [BsonIgnoreExtraElements]
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string? Name { get; set; }
    }
}
