using api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Contracts.IRepositories
{
    public interface IRecurringTransactionRepository
    {
        Task<List<RecurringTransactionModel>> GetByWallet(string walletId);

        Task<List<RecurringTransactionModel>> GetByUser(string userId);

        Task<RecurringTransactionModel> Get(string id);

        Task Create(RecurringTransactionModel recurringTransaction);

        Task Update(string id, RecurringTransactionModel recurringTransaction);

        Task Remove(string id);
    }
}
