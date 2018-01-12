using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BetterGeekApi.Model
{
    public class Game
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public int GameId { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Thumbnail { get; set; }
        public int MinPlayers { get; set; }
        public int MaxPlayers { get; set; }
        public int PlayingTime { get; set; }
        public bool IsExpansion { get; set; }
        public int YearPublished { get; set; }
        public float AverageRating { get; set; }
        public int Rank { get; set; }
        public int NumPlayers { get; set; }

        public DateTime UpdatedOn { get; set; } = DateTime.Now;
        public DateTime CreatedOn { get; set; } = DateTime.Now;
    }
}