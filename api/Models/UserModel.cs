using api.Enums;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class UserModel
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string id { get; set; }
        public string name { get; set; }
        public DateTime dob { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public Currency currency { get; set; }

        public UserModel(string name, DateTime dob, string email, string password, Currency currency)
        {
            this.name = name;
            this.dob = dob;
            this.email = email;
            this.password = password;
            this.currency = currency;
        }
    }
}
