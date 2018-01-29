using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BetterGeekApi.Model
{
    public class User : Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Image { get; set; }
        public string Thumbnail { get; set; }
        public string BGGUserName { get; set; }
        public string Email { get; set; }
        public IEnumerable<string> GameCollection { get; set; }

      }
}