﻿using api.Contracts.IRepositories;
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
        public async Task Create(UserModel user)
        {
            user.Role = "user";
            await _userRepository.Create(user);
        }
        public async Task Update(string id, UserModel user)
        {
            await _userRepository.Update(id, user);
        }
        public async Task Remove(UserModel user)
        {
            await _userRepository.Remove(user);
        }
        public async Task Remove(string id)
        {
            await _userRepository.Remove(id);
        }

        public async Task<UserModel> Authenticate(CredentialsModel credentials)
        {
            UserModel user = await _userRepository.GetByEmail(credentials.Email);

            if (user != null && user.Password == credentials.Password)
                return user;
            else return null;
        }
        public async Task CreateAccount(string id, AccountModel account)
        {
            UserModel User = await _userRepository.Get(id);
            User.Accounts.Add(account);

            await _userRepository.Update(id, User);
        }
        public async Task DeleteAccount(string id, string accountId)
        {
            UserModel User = await _userRepository.Get(id);

            User.Accounts.Remove(User.Accounts.Where(a => a.Id == accountId).First());
            await _userRepository.Update(id, User);
        }
    }
}
