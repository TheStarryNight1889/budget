using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Contracts.IRepositories
{
    public interface IRepository<T,U>
        where T : class
        where U : class
    {
        public Task<List<T>> Get();
        public Task<T> Get(string id);
        public Task Create(U obj);
        public Task Update(string email, U obj);
        public Task Remove(U obj);
        public Task Remove(string id);
    }
}
