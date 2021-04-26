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
    public class TransactionRepository : ITransactionsRepository
    {
        private readonly IMongoCollection<TransactionModel> _transactions;

        public TransactionRepository(IBudgetDataBaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _transactions = database.GetCollection<TransactionModel>(settings.TransactionsCollectionName);
        }

        public async Task<List<TransactionModel>> GetByWallet(string walletId) => await _transactions.Find(transaction => transaction.WalletId == walletId).ToListAsync();

        public async Task<List<TransactionModel>> GetByUser(string userId) => await _transactions.Find(transaction => transaction.UserId == userId).ToListAsync();

        public async Task<TransactionModel> Get(string id) => await _transactions.Find(transaction => transaction._id == id).SingleAsync<TransactionModel>();

        public async Task Create(TransactionModel transaction) => await _transactions.InsertOneAsync(transaction);

        public async Task Update(string id, TransactionModel transaction) => await _transactions.ReplaceOneAsync(transaction => transaction._id == id, transaction);

        public async Task Remove(string id) => await _transactions.DeleteOneAsync(transaction => transaction._id == id);
    }
}
