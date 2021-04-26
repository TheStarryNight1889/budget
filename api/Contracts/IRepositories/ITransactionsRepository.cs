using api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Contracts.IRepositories
{
    public interface ITransactionsRepository
    {
        public Task<List<TransactionModel>> GetByWallet(string walletId);
        public Task<List<TransactionModel>> GetByUser(string userId);
        public Task<TransactionModel> Get(string id);
        public Task Create(TransactionModel transaction);
        public Task Update(string id, TransactionModel transaction);
        public Task Remove(string id);
    }
}
