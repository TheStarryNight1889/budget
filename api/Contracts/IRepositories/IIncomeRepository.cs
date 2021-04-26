using api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Contracts.IRepositories
{
    public interface IIncomeRepository
    {
        Task<List<IncomeModel>> GetByWallet(string walletId);

        Task<List<IncomeModel>> GetByUser(string userId);

        Task<IncomeModel> Get(string id);

        Task Create(IncomeModel income);

        Task Update(string id, IncomeModel income);

        Task Remove(string id);
    }
}
