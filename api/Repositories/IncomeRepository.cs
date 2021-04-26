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
    public class IncomeRepository : IIncomeRepository
    {
        private readonly IMongoCollection<IncomeModel> _income;

        public IncomeRepository(IBudgetDataBaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _income = database.GetCollection<IncomeModel>(settings.IncomeCollectionName);
        }

        public async Task<List<IncomeModel>> GetByWallet(string walletId) => await _income.Find(income => income.WalletId == walletId).ToListAsync();

        public async Task<List<IncomeModel>> GetByUser(string userId) => await _income.Find(income => income.UserId == userId).ToListAsync();

        public async Task<IncomeModel> Get(string id) => await _income.Find(income => income._id == id).SingleAsync<IncomeModel>();

        public async Task Create(IncomeModel income) => await _income.InsertOneAsync(income);

        public async Task Update(string id, IncomeModel income) => await _income.ReplaceOneAsync(income => income._id == id, income);

        public async Task Remove(string id) => await _income.DeleteOneAsync(income => income._id == id);
    }
}
