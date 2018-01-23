using MongoDB.Bson;
using System;
using MongoDB.Bson.Serialization.Attributes;

namespace BetterGeekApi.Model
{
    public abstract class Entity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public DateTime UpdatedDate { get; set; } = DateTime.Now;
    }
}