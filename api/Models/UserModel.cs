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
        public string Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public DateTime DOB { get; set; }
        public string Password { get; set; }
        public Currency Currency { get; set; }
        public string Role { get; set; }
        public List<AccountModel> Accounts { get ; set; }
        public List<string> TargetIds { get; set; }

        public UserModel(string name, DateTime dob, string email, string password, Currency currency)
        {
            this.Name = name;
            this.DOB = dob;
            this.Email = email;
            this.Password = password;
            this.Currency = currency;
        }
    }
}
