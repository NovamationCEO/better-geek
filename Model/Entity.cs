using MongoDB.Bson;
using System;

namespace BetterGeekApi.Model
{
    public abstract class Entity
    {
        public ObjectId Id { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public DateTime UpdatedDate { get; set; } = DateTime.Now;
    }
}