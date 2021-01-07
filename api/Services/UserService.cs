using api.Contracts.IRepositories;
using api.Enums;
using api.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Services
{
    public class UserService
    {
        private readonly IRepository<UserModel,UserModel> _userRepository;

        public UserService(IRepository<UserModel,UserModel> userRepository)
        {
            this._userRepository = userRepository;
        }
        public JObject GetAll()
        {
            JObject json = new JObject();
            json["users"] = JToken.FromObject(_userRepository.Get());
            return json;
        }
        public JObject Get(string id)
        {
            JObject json = new JObject();
            json["users"] = JToken.FromObject(_userRepository.Get(id));
            return json;
        }
        public void Create(JObject user)
        {
            _userRepository.Create(UserFactory(user));
        }
        public void Update(string id, JObject user)
        {
            _userRepository.Update(id, UserFactory(user));
        }
        public void Remove(JObject user)
        {
            _userRepository.Remove(UserFactory(user));
        }
        public void Remove(string id)
        {
            _userRepository.Remove(id);
        }

        public UserModel UserFactory(JObject user)
        {
            UserModel nu = new UserModel(
                user.GetValue("name").ToString(),
                Convert.ToDateTime(user.GetValue("dob")),
                user.GetValue("email").ToString(),
                user.GetValue("password").ToString(),
                (Currency)Convert.ToInt32(user.GetValue("currency"))
                );
            return nu;
        }
    }
}
