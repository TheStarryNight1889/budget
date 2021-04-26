using api.Contracts.IModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class BudgetDataBaseSettings : IBudgetDataBaseSettings
    {
        public string UsersCollectionName { get; set; }
        public string IncomeCollectionName { get; set; }
        public string TransactionsCollectionName { get; set; }
        public string RecurringTransactionsCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
