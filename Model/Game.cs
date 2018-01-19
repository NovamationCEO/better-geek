using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BetterGeekApi.Model
{
    public class Game : Entity
    {
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
      }
}