using api.Enums;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class RecurringTransactionModel
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string _id { get; set; }
        public string UserId { get; set; }
        public string WalletId { get; set; }
        public string Name { get; set; }
        public double Amount { get; set; }
        public int BillingDate { get; set; }
        public TransactionCategory Category { get; set; }
        //Bill can change value each billing period (usually does)
        //Subscription is the same each billing period
        public RecurringType Type { get; set; }

        public RecurringTransactionModel(string userId, string walletId, string name, double amount, int recurringDate, TransactionCategory category, RecurringType type)
        {
            UserId = userId;
            WalletId = walletId;
            Name = name;
            Amount = amount;
            BillingDate = recurringDate;
            Category = category;
            Type = type;
        }

    }
}
