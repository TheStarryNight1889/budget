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
        public async Task<List<UserModel>> GetAll()
        {
            return await _userRepository.Get();
        }
        public async Task<UserModel> Get(string id)
        {
            return await _userRepository.Get(id);
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
            try
            {
                UserModel oldUser = await _userRepository.Get(id);
                user.Wallets = oldUser.Wallets;
                user.Role = "user";
                await _userRepository.Update(id, user);
            } catch(Exception e) { throw new Exception(); }
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
    }
}
