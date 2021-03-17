using api.Enums;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class AccountModel
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public AccountType Type { get; set; }
        public bool Default { get; set; }
        public double Balance { get; set; }
        public Dictionary<DateTime, double> DateOffsetBalance { get; set; }
        public AccountColor Color { get; set; }
        public DateTime LastUpdated { get; set; }
        public List<string> TransactionIds { get; set; }
        public List<string> RecurringTransactionIds { get; set; }

        public AccountModel(string userId, string name, AccountType type, bool @default, double balance, Dictionary<DateTime, double> dateOffsetBalance, AccountColor color, DateTime lastUpdated, List<string> transactionIds, List<string> recurringTransactionIds)
        {
            UserId = userId;
            Name = name;
            Type = type;
            Default = @default;
            Balance = balance;
            DateOffsetBalance = dateOffsetBalance;
            Color = color;
            LastUpdated = lastUpdated;
            TransactionIds = transactionIds;
            RecurringTransactionIds = recurringTransactionIds;
        }
    }
}
