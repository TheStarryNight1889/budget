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
        public List<T> Get();
        public T Get(string id);
        public void Create(U obj);
        public void Update(string id, U obj);
        public void Remove(U obj);
        public void Remove(string id);
    }
}
