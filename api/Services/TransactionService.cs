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
    public class TransactionService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITransactionsRepository _transactionRepository;

        public TransactionService() { }
        public TransactionService(IUserRepository userRepository, ITransactionsRepository transactionRepository)
        {
            this._userRepository = userRepository;
            this._transactionRepository = transactionRepository;
        }
        public async Task<List<TransactionModel>> GetTransactionsForWallet(string userId, string walletId)
        {
            try
            {
                UserModel user = await _userRepository.Get(userId);
                if (!user.Wallets.Exists(wallet => wallet._id == walletId))
                    throw new KeyNotFoundException();
                return await _transactionRepository.GetByWallet(walletId);
            } catch(Exception e)
            {
                throw new Exception();
            }
        }
        public async Task<List<TransactionModel>> GetTransactionsForUser(string userId)
        {
            try
            {
                return await _transactionRepository.GetByUser(userId);
            } catch(Exception e)
            {
                throw new Exception();
            }
        }

        public async Task CreateTransaction(string userId, TransactionModel transaction)
        {
            try
            {
                transaction.UserId = userId;

                UserModel user = await _userRepository.Get(userId);
                user.Wallets.Add(await UpdateWalletForNewTransaction(user, transaction));

                await _transactionRepository.Create(transaction);
                await _userRepository.Update(userId, user);
            } catch(Exception e)
            {
                throw new Exception();
            }
        }
        public async Task UpdateTransaction(string userId, TransactionModel transaction)
        {
            try
            {
                UserModel user = await _userRepository.Get(userId);
                TransactionModel oldTransaction = await _transactionRepository.Get(transaction._id);

                if (transaction.Amount != oldTransaction.Amount)
                {
                    user.Wallets.Add(await UpdateWalletForUpdatedTransaction(user, transaction, oldTransaction));
                }

                await _transactionRepository.Update(transaction._id, transaction);
                await _userRepository.Update(userId, user);
            } catch(Exception e)
            {
                throw new Exception();
            }
        }
        public async Task DeleteTransaction(string userId, string transactionId)
        {
            try
            {
                UserModel user = await _userRepository.Get(userId);
                TransactionModel transaction = await _transactionRepository.Get(transactionId);

                user.Wallets.Add(await UpdateWalletForDeletedTransaction(user, transaction));
                await _transactionRepository.Remove(transactionId);
                await _userRepository.Update(userId, user);
            } catch(Exception e)
            {
                throw new Exception();
            }
        }
        public async Task<WalletModel> UpdateWalletForNewTransaction(UserModel user, TransactionModel transaction)
        {
            WalletModel wallet = user.Wallets.Where(wallet => wallet._id == transaction.WalletId).First();
            wallet.Balance -= transaction.Amount;
            wallet.LastUpdated = DateTime.UtcNow;

            //If there is no entry for today. make one. else update todays date with new balance
            if (!wallet.DateOffsetBalance.Exists(dateoffsetbalance => dateoffsetbalance.Date.Date == DateTime.UtcNow.Date))
                wallet.DateOffsetBalance.Add(new DateOffsetBalance(DateTime.UtcNow, wallet.Balance));
            else
                wallet.DateOffsetBalance.Where(dateoffsetbalance => dateoffsetbalance.Date.Date == DateTime.UtcNow.Date).First().Balance = wallet.Balance;

            user.Wallets.Remove(user.Wallets.Where(a => a._id == wallet._id).First());
            return wallet;
        }
        public async Task<WalletModel> UpdateWalletForUpdatedTransaction(UserModel user, TransactionModel newTransaction, TransactionModel oldTransaction)
        {
            double difference = newTransaction.Amount - oldTransaction.Amount;
            WalletModel wallet = user.Wallets.Where(wallet => wallet._id == oldTransaction.WalletId).First();
            wallet.Balance -= difference;
            wallet.LastUpdated = DateTime.UtcNow;

            try
            {
                wallet.DateOffsetBalance.Where(dateoffsetbalance => dateoffsetbalance.Date.Date == oldTransaction.Date.Date).First().Balance -= difference;
            }
            catch (Exception e) { }
            user.Wallets.Remove(user.Wallets.Where(a => a._id == wallet._id).First());
            return wallet;
        }
        public async Task<WalletModel> UpdateWalletForDeletedTransaction(UserModel user, TransactionModel transactionModel)
        {
            WalletModel wallet = user.Wallets.Where(wallet => wallet._id == transactionModel.WalletId).First();
            wallet.Balance += transactionModel.Amount;
            wallet.LastUpdated = DateTime.UtcNow;

            try
            {
                wallet.DateOffsetBalance.Where(dateoffsetbalance => dateoffsetbalance.Date.Date == transactionModel.Date.Date).First().Balance += transactionModel.Amount;
            }
            catch (Exception e) { }
            user.Wallets.Remove(user.Wallets.Where(a => a._id == wallet._id).First());
            return wallet;
        }

    }
}
