using api.Contracts.IModels;
using api.Contracts.IRepositories;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Repositories
{
    public class Repository : IRepository
    {
        protected IMongoDatabase database;
        public Repository(IBudgetDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            this.database = client.GetDatabase(settings.DatabaseName);
        }
    }
}
