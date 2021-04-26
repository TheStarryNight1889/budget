using api.Contracts.IRepositories;
using api.Models;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Services
{
    public class IncomeService
    {
        private readonly IUserRepository _userRepository;
        private readonly IIncomeRepository _incomeRepository;

        public IncomeService() { }
        public IncomeService(IUserRepository userRepository, IIncomeRepository incomeRepository)
        {
            this._userRepository = userRepository;
            this._incomeRepository = incomeRepository;
        }
        public async Task<List<IncomeModel>> GetIncomeForWallet(string userId, string walletId)
        {
            try
            {
                UserModel user = await _userRepository.Get(userId);
                if (!user.Wallets.Exists(wallet => wallet._id == walletId))
                    throw new KeyNotFoundException();
                return await _incomeRepository.GetByWallet(walletId);
            }
            catch (Exception e)
            {
                throw new Exception();
            }
        }
        public async Task<List<IncomeModel>> GetIncomeForUser(string userId)
        {
            try
            {
                return await _incomeRepository.GetByUser(userId);
            }
            catch (Exception e)
            {
                throw new Exception();
            }
        }

        public async Task CreateIncome(string userId, IncomeModel income)
        {
            try
            {
                income.UserId = userId;

                UserModel user = await _userRepository.Get(userId);
                user.Wallets.Add(await UpdateWalletForNewIncome(user, income));

                await _incomeRepository.Create(income);
                await _userRepository.Update(userId, user);
            }
            catch (Exception e)
            {
                throw new Exception();
            }
        }
        public async Task UpdateIncome(string userId, IncomeModel income)
        {
            try
            {
                UserModel user = await _userRepository.Get(userId);
                IncomeModel oldIncome = await _incomeRepository.Get(income._id);

                if(income.Amount != oldIncome.Amount)
                {
                    user.Wallets.Add(await UpdateWalletForUpdatedIncome(user, income, oldIncome));
                }

                
                await _incomeRepository.Update(income._id, income);
                await _userRepository.Update(userId, user);
            }
            catch (Exception e)
            {
                throw new Exception();
            }
        }
        public async Task DeleteIncome(string userId, string incomeId)
        {
            try
            {
                UserModel user = await _userRepository.Get(userId);
                IncomeModel income = await _incomeRepository.Get(incomeId);

                user.Wallets.Add(await UpdateWalletForDeletedIncome(user, income));
                await _incomeRepository.Remove(incomeId);
                await _userRepository.Update(userId, user);
            }
            catch (Exception e)
            {
                throw new Exception();
            }
        }
        public async Task<WalletModel> UpdateWalletForNewIncome(UserModel user, IncomeModel income)
        {
            WalletModel wallet = user.Wallets.Where(wallet => wallet._id == income.WalletId).First();
            wallet.Balance += income.Amount;
            wallet.LastUpdated = DateTime.UtcNow;

            //If there is no entry for today. make one. else update todays date with new balance
            if (!wallet.DateOffsetBalance.Exists(dateoffsetbalance => dateoffsetbalance.Date.Date == DateTime.UtcNow.Date))
                wallet.DateOffsetBalance.Add(new DateOffsetBalance(DateTime.UtcNow, wallet.Balance));
            else
                wallet.DateOffsetBalance.Where(dateoffsetbalance => dateoffsetbalance.Date.Date == DateTime.UtcNow.Date).First().Balance = wallet.Balance;

            user.Wallets.Remove(user.Wallets.Where(a => a._id == wallet._id).First());
            return wallet; 
        }
        public async Task<WalletModel> UpdateWalletForUpdatedIncome(UserModel user, IncomeModel newIncome, IncomeModel oldIncome)
        {
            double difference = newIncome.Amount - oldIncome.Amount;
            WalletModel wallet = user.Wallets.Where(wallet => wallet._id == oldIncome.WalletId).First();
            wallet.Balance += difference;
            wallet.LastUpdated = DateTime.UtcNow;

            try
            {
                wallet.DateOffsetBalance.Where(dateoffsetbalance => dateoffsetbalance.Date.Date == oldIncome.Date.Date).First().Balance += difference;
            }
            catch (Exception e) { }
            user.Wallets.Remove(user.Wallets.Where(a => a._id == wallet._id).First());
            return wallet;
        }
        public async Task<WalletModel> UpdateWalletForDeletedIncome(UserModel user, IncomeModel income)
        {
            WalletModel wallet = user.Wallets.Where(wallet => wallet._id == income.WalletId).First();
            wallet.Balance -= income.Amount;
            wallet.LastUpdated = DateTime.UtcNow;

            try
            {
                wallet.DateOffsetBalance.Where(dateoffsetbalance => dateoffsetbalance.Date.Date == income.Date.Date).First().Balance -= income.Amount;
            }
            catch (Exception e) { }
            user.Wallets.Remove(user.Wallets.Where(a => a._id == wallet._id).First());
            return wallet;
        }
    }
}
