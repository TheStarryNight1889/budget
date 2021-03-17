using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class TargetModel
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        public string UserId { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ExpectedEndDate { get; set; }
        public DateTime ActualEndDate { get; set; }
        public double Goal { get; set; }
        public Dictionary<DateTime,double> DateOffsetProgress { get; set; }
        public bool GoalMet { get; set; }
        public double Amount { get; set; }
        public string Name { get; set; }

        public TargetModel(string userId, DateTime creationDate, DateTime expectedEndDate, DateTime actualEndDate, double goal, Dictionary<DateTime, double> dateOffsetProgress, bool goalMet, double amount, string name)
        {
            UserId = userId;
            CreationDate = creationDate;
            ExpectedEndDate = expectedEndDate;
            ActualEndDate = actualEndDate;
            Goal = goal;
            DateOffsetProgress = dateOffsetProgress;
            GoalMet = goalMet;
            Amount = amount;
            Name = name;
        }
    }
}
