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
    public class RecurringTransactionRepository : IRecurringTransactionRepository
    {
        private readonly IMongoCollection<RecurringTransactionModel> _recurringTransactions;

        public RecurringTransactionRepository(IBudgetDataBaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _recurringTransactions = database.GetCollection<RecurringTransactionModel>(settings.RecurringTransactionsCollectionName);
        }

        public async Task<List<RecurringTransactionModel>> GetByWallet(string walletId) => await _recurringTransactions.Find(recurringTransaction => recurringTransaction.WalletId == walletId).ToListAsync();

        public async Task<List<RecurringTransactionModel>> GetByUser(string userId) => await _recurringTransactions.Find(recurringTransaction => recurringTransaction.UserId == userId).ToListAsync();

        public async Task<RecurringTransactionModel> Get(string id) => await _recurringTransactions.Find(transaction => transaction._id == id).SingleAsync<RecurringTransactionModel>();

        public async Task Create(RecurringTransactionModel recurringTransaction) => await _recurringTransactions.InsertOneAsync(recurringTransaction);

        public async Task Update(string id, RecurringTransactionModel recurringTransaction) => await _recurringTransactions.ReplaceOneAsync(recurringTransaction => recurringTransaction._id == id, recurringTransaction);

        public async Task Remove(string id) => await _recurringTransactions.DeleteOneAsync(transaction => transaction._id == id);
    }
}
