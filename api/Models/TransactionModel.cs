using api.Enums;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class TransactionModel
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        public double Amount { get; set; }
        public DateTime Date { get; set; }
        public TransactionCategory Category { get; set; }
        public string Name { get; set; }
        public string Store { get; set; }
        public string Goods { get; set; }

        public TransactionModel(double amount, DateTime date, TransactionCategory category, string name, string store, string goods)
        {
            Amount = amount;
            Date = date;
            Category = category;
            Name = name;
            Store = store;
            Goods = goods;
        }

    }
}
