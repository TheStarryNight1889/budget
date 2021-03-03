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
        public async Task<JObject> GetAll()
        {
            try
            {
                JObject json = new JObject
                {
                    ["users"] = JToken.FromObject(await _userRepository.Get())
                };
                return json;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }
        public async Task<JObject> Get(string email)
        {
            try
            {
                var user = await _userRepository.Get(email);

                JObject json = new JObject
                {
                    ["user"] = JToken.FromObject(user)
                };
                return json;
            }
            catch(Exception e)
            {
                //Console.WriteLine(e);
                return null;
            }

        }
        public async Task Create(JObject user)
        {
            UserModel userModel = UserFactory(user);
            userModel.Role = "user";
            await _userRepository.Create(userModel);
        }
        public async Task Update(string email, JObject user)
        {
            UserModel nu = UserFactory(user);
            nu.Id = user.GetValue("id").ToString();
            nu.Email = email;
            await _userRepository.Update(email, nu);
        }
        public async Task Remove(JObject user)
        {
            await _userRepository.Remove(UserFactory(user));
        }
        public async Task Remove(string email)
        {
            await _userRepository.Remove(email);
        }

        public async Task<UserModel> Authenticate(JObject credentials)
        {
            UserModel user = await _userRepository.Get(credentials.GetValue("email").ToString());

            if (user != null && user.Password == credentials.GetValue("password").ToString())
                return user;
            else return null;
        }
        public UserModel UserFactory(JObject user)
        {
            UserModel nu = new UserModel(
                user.GetValue("name").ToString(),
                DateTime.ParseExact(user.GetValue("dob").ToString(), "yyyy-MM-dd", provider: null),
                user.GetValue("email").ToString(),
                user.GetValue("password").ToString(),
                (Currency)Convert.ToInt32(user.GetValue("currency"))
                );
            return nu;
        }
    }
}
