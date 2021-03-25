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
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
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
        public async Task<JObject> Get(string id)
        {
            try
            {
                var user = await _userRepository.Get(id);

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
        public async Task<bool> IsEmailIsAvailable(string email)
        {
            try
            {
                await _userRepository.GetByEmail(email);
                return false;
            }
            catch(Exception e)
            {
                return true;
            }
        }
        public async Task Create(JObject user)
        {
            UserModel userModel = UserFactory(user);
            userModel.Role = "user";
            await _userRepository.Create(userModel);
        }
        public async Task Update(string id, JObject user)
        {
            UserModel nu = UserFactory(user);
            nu.Id = user.GetValue("id").ToString();

            await _userRepository.Update(id, nu);
        }
        public async Task Remove(JObject user)
        {
            await _userRepository.Remove(UserFactory(user));
        }
        public async Task Remove(string id)
        {
            await _userRepository.Remove(id);
        }

        public async Task<UserModel> Authenticate(JObject credentials)
        {
            UserModel user = await _userRepository.GetByEmail(credentials.GetValue("email").ToString());

            if (user != null && user.Password == credentials.GetValue("password").ToString())
                return user;
            else return null;
        }
        public async Task CreateAccount(string id, JObject account)
        {
            UserModel User = await _userRepository.Get(id);
            User.Accounts.Add(AccountFactory(account));

            await _userRepository.Update(id, User);
        }
        public async Task DeleteAccount(string id, JObject account)
        {
            UserModel User = await _userRepository.Get(id);
            
            foreach(AccountModel accountModel in User.Accounts)
            {
                if (accountModel.Id == AccountFactory(account).Id)
                {
                    account = null;
                }
            }
            await _userRepository.Update(id, User);
        }
        public UserModel UserFactory(JObject user)
        {
            UserModel User = user.ToObject<UserModel>();
            return User;
        }
        public AccountModel AccountFactory(JObject account)
        {
            AccountModel Account = account.ToObject<AccountModel>();
            return Account;
        }
    }
}
