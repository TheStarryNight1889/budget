using api.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class WalletModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
        public string Name { get; set; }
        public WalletType Type { get; set; }
        public bool Default { get; set; }
        public double Balance { get; set; }
        public List<DateOffsetBalance> DateOffsetBalance { get; set; }
        public WalletColor Color { get; set; }
        public DateTime LastUpdated { get; set; }

        public WalletModel(string name, WalletType type, bool @default, double balance, List<DateOffsetBalance> dateOffsetBalance, WalletColor color, DateTime lastUpdated)
        {
            Name = name;
            Type = type;
            Default = @default;
            Balance = balance;
            DateOffsetBalance = dateOffsetBalance;
            Color = color;
            LastUpdated = lastUpdated;
        }
    }
}
