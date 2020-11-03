using api.Contracts.IModels;

namespace api.Models
{
    public class BudgetDatabaseSettings : IBudgetDatabaseSettings
    {
        public string UsersCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
