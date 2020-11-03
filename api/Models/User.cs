using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace api.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }
        public string name { get; set; }
        public DateTime dob { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string currency { get; set; }
    }
}
