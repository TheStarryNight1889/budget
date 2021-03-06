﻿using api.Contracts.IRepositories;
using api.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Services
{
    public class WalletService
    {
        private readonly IUserRepository _userRepository;

        public WalletService(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }
        public async Task<List<WalletModel>> GetWallets(string userId)
        {
            try
            {
                UserModel user = await _userRepository.Get(userId);
                return user.Wallets;

            } catch(Exception e)
            {
                throw new Exception();
            }
        }
        public async Task CreateWallet(string id, WalletModel wallet)
        {
            try
            {
                UserModel User = await _userRepository.Get(id);
                wallet.LastUpdated = DateTime.UtcNow;
                // the UUID is manually assigned because MongoDB will not autimatically generate ID's for sub-documents
                wallet._id = ObjectId.GenerateNewId().ToString();
                User.Wallets.Add(wallet);

                await _userRepository.Update(id, User);
            } catch(Exception e)
            {
                throw new Exception();
            }
        }
        public async Task UpdateWallet(string id, WalletModel wallet)
        {
            try
            {
                UserModel User = await _userRepository.Get(id);

                User.Wallets.Remove(User.Wallets.Where(a => a._id == wallet._id).First());
                User.Wallets.Add(wallet);
                await _userRepository.Update(id, User);
            } catch(Exception e)
            {
                throw new Exception();
            }
        }
        public async Task DeleteWallet(string id, string walletId)
        {
            try
            {
                UserModel User = await _userRepository.Get(id);

                User.Wallets.Remove(User.Wallets.Where(a => a._id == walletId).First());
                await _userRepository.Update(id, User);
            } catch(Exception e)
            {
                throw new Exception();
            }
        }
    }
}
