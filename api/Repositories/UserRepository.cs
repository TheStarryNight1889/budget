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

        public async Task<List<UserModel>> Get() => (List<UserModel>)await _users.FindAsync(book => true);

        public async Task<UserModel> Get(string email) => (UserModel)await _users.FindAsync<UserModel>(user => user.Email == email);

        public async Task Create(UserModel user) => await _users.InsertOneAsync(user);

        public async Task Update(string email, UserModel userIn) => await _users.ReplaceOneAsync(user => user.Email == email, userIn);

        public async Task Remove(UserModel userIn) => await _users.DeleteOneAsync(user => user.Email == userIn.Email);

        public async Task Remove(string email) => await _users.DeleteOneAsync(user => user.Email == email);
    }
}
