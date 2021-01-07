using api.Contracts.IModels;
using api.Contracts.IRepositories;
using api.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Repositories
{
    public class UserRepository : IRepository<UserModel, UserModel>
    {
        private readonly IMongoCollection<UserModel> _users;

        public UserRepository(IBudgetDataBaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _users = database.GetCollection<UserModel>(settings.UsersCollectionName);
        }

        public List<UserModel> Get() => _users.Find(book => true).ToList();

        public UserModel Get(string id) => _users.Find<UserModel>(user => user.id == id).FirstOrDefault();

        public void Create(UserModel user) => _users.InsertOne(user);

        public void Update(string id, UserModel userIn) => _users.ReplaceOne(user => user.id == id, userIn);

        public void Remove(UserModel userIn) => _users.DeleteOne(book => book.id == userIn.id);

        public void Remove(string id) => _users.DeleteOne(book => book.id == id);
    }
}
