using api.Contracts.IRepositories;
using api.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Services
{
    public class RecurringTransactionService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRecurringTransactionRepository _recurringTransactionsRepository;

        public RecurringTransactionService() { }
        public RecurringTransactionService(IUserRepository userRepository, IRecurringTransactionRepository recurringTransactionsRepository)
        {
            this._userRepository = userRepository;
            this._recurringTransactionsRepository = recurringTransactionsRepository;
        }
        public async Task<List<RecurringTransactionModel>> GetRecurringTransactionsForWallet(string userId, string walletId)
        {
            try
            {
                UserModel user = await _userRepository.Get(userId);
                if (!user.Wallets.Exists(wallet => wallet._id == walletId))
                    throw new KeyNotFoundException();
                return await _recurringTransactionsRepository.GetByWallet(walletId);
            }
            catch (Exception e)
            {
                throw new Exception();
            }
        }
        public async Task<List<RecurringTransactionModel>> GetRecurringTransactionsForUser(string userId)
        {
            try
            {
                return await _recurringTransactionsRepository.GetByUser(userId);
            }
            catch (Exception e)
            {
                throw new Exception();
            }
        }

        public async Task CreateRecurringTransaction(string userId, RecurringTransactionModel recurringTransaction)
        {
            try
            {
                recurringTransaction.UserId = userId;
                await _recurringTransactionsRepository.Create(recurringTransaction);
            }
            catch (Exception e)
            {
                throw new Exception();
            }
        }
        public async Task UpdateRecurringTransaction(RecurringTransactionModel recurringTransaction)
        {
            try
            {
                await _recurringTransactionsRepository.Update(recurringTransaction._id, recurringTransaction);
            }
            catch (Exception e)
            {
                throw new Exception();
            }
        }
        public async Task DeleteRecurringTransaction(string transactionId)
        {
            try
            {
                await _recurringTransactionsRepository.Remove(transactionId);
            }
            catch (Exception e)
            {
                throw new Exception();
            }
        }
        public async Task<WalletModel> UpdateWalletForNewRecurringTransaction(UserModel user, RecurringTransactionModel recurringTransaction)
        {
            WalletModel wallet = user.Wallets.Where(wallet => wallet._id == recurringTransaction.WalletId).First();
            wallet.Balance -= recurringTransaction.Amount;
            wallet.LastUpdated = DateTime.UtcNow;

            //If there is no entry for today. make one. else update todays date with new balance
            if (!wallet.DateOffsetBalance.Exists(dateoffsetbalance => dateoffsetbalance.Date.Date == DateTime.UtcNow.Date))
                wallet.DateOffsetBalance.Add(new DateOffsetBalance(DateTime.UtcNow, wallet.Balance));
            else
                wallet.DateOffsetBalance.Where(dateoffsetbalance => dateoffsetbalance.Date.Date == DateTime.UtcNow.Date).First().Balance = wallet.Balance;

            user.Wallets.Remove(user.Wallets.Where(a => a._id == wallet._id).First());
            return wallet;
        }
        public async Task<WalletModel> UpdateWalletForUpdatedRecurringTransaction(UserModel user, RecurringTransactionModel newRecurringTransaction, RecurringTransactionModel oldRecurringTransaction)
        {
            double difference = newRecurringTransaction.Amount - oldRecurringTransaction.Amount;
            WalletModel wallet = user.Wallets.Where(wallet => wallet._id == oldRecurringTransaction.WalletId).First();
            wallet.Balance -= difference;
            wallet.LastUpdated = DateTime.UtcNow;

            user.Wallets.Remove(user.Wallets.Where(a => a._id == wallet._id).First());
            return wallet;
        }
        public async Task<WalletModel> UpdateWalletForDeletedRecurringTransaction(UserModel user, RecurringTransactionModel recurringTransactionModel)
        {
            WalletModel wallet = user.Wallets.Where(wallet => wallet._id == recurringTransactionModel.WalletId).First();
            wallet.Balance += recurringTransactionModel.Amount;
            wallet.LastUpdated = DateTime.UtcNow;

            user.Wallets.Remove(user.Wallets.Where(a => a._id == wallet._id).First());
            return wallet;
        }
    }
}
