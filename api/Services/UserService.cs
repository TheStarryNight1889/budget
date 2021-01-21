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
            JObject json = new JObject
            {
                ["users"] = JToken.FromObject(_userRepository.Get())
            };
            return json;
        }
        public JObject Get(string email)
        {
            JObject json = new JObject
            {
                ["user"] = JToken.FromObject(_userRepository.Get(email))
            };
            return json;
        }
        public void Create(JObject user)
        {
            _userRepository.Create(UserFactory(user));
        }
        public void Update(string email, JObject user)
        {
            UserModel nu = UserFactory(user);
            nu.id = user.GetValue("id").ToString();
            nu.email = email;
            _userRepository.Update(email, nu);
        }
        public void Remove(JObject user)
        {
            _userRepository.Remove(UserFactory(user));
        }
        public void Remove(string email)
        {
            _userRepository.Remove(email);
        }

        public UserModel Authenticate(JObject credentials)
        {
            UserModel user = _userRepository.Get(credentials.GetValue("email").ToString());

            if (user != null && user.password == credentials.GetValue("password").ToString())
                return user;
            else return null;
        }
        public UserModel UserFactory(JObject user)
        {
            UserModel nu = new UserModel(
                user.GetValue("name").ToString(),
                DateTime.ParseExact(user.GetValue("dob").ToString(), "dd/MM/yyyy", provider: null),
                user.GetValue("email").ToString(),
                user.GetValue("password").ToString(),
                (Currency)Convert.ToInt32(user.GetValue("currency"))
                );
            return nu;
        }
    }
}
