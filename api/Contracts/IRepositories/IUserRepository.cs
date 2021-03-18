using api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Contracts.IRepositories
{
    public interface IUserRepository
    {
        public Task<List<UserModel>> Get();
        public Task<UserModel> Get(string id);
        public Task<UserModel> GetByEmail(string email);
        public Task Create(UserModel user);
        public Task Update(string id, UserModel user);
        public Task Remove(UserModel user);
        public Task Remove(string id);
    }
}
