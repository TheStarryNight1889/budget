using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Contracts.IModels
{
    public interface IBudgetDataBaseSettings
    {
        string UsersCollectionName { get; set; }
        string TransactionsCollectionName { get; set; }
        string IncomeCollectionName { get; set; }
        string RecurringTransactionsCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
