using api.Contracts.IModels;
using api.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Repositories
{
    public class UserRepository : Repository
    {
        private readonly IMongoCollection<User> _users;

        public UserRepository(IBudgetDatabaseSettings settings) : base(settings)
        {
            _users = database.GetCollection<User>(settings.UsersCollectionName);
        }
    }
}
