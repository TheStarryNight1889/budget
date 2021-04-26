using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class IncomeModel
    { 
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string _id { get; set; }
        public string UserId { get; set; }
        public string WalletId { get; set; }
        public double Amount { get; set; }
        public DateTime Date { get; set; }
        public string Name { get; set; }

        public IncomeModel(string userId, string walletId, double amount, DateTime date, string name)
        {
            UserId = userId;
            WalletId = walletId;
            Amount = amount;
            Date = date;
            Name = name;
        }
    }
}
