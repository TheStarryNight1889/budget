using api.Models;
using api.Services;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests
{
    public class RecurringTransactionServiceTests
    {
        [Fact]
        public async Task RecurringTransactionService_Updates_Wallet_New_RecurringTransaction()
        {
            DateTime now = DateTime.UtcNow;

            UserModel user = new UserModel("Test User", new DateTime(), "randomemail@gmail.com", "password", api.Enums.Currency.Euro);
            user._id = "1234567891234567891234";
            user.Role = "user";

            WalletModel wallet = new WalletModel("Unit Test", 0, true, 100, null, api.Enums.WalletColor.Green, now);
            wallet._id = "1234567891234567891234";
            wallet.DateOffsetBalance = new List<DateOffsetBalance>();

            user.Wallets.Add(wallet);

            RecurringTransactionModel recurringTransactionModel = new RecurringTransactionModel(user._id, wallet._id, "Netflix", 100, 27, api.Enums.TransactionCategory.Entertainment, api.Enums.RecurringType.Subscription);

            RecurringTransactionService service = new RecurringTransactionService();
            WalletModel newWallet = await service.UpdateWalletForNewRecurringTransaction(user, recurringTransactionModel);
            newWallet.Balance.Should().Be(0);
            newWallet.LastUpdated.Should().NotBeOnOrBefore(now);
            newWallet.DateOffsetBalance.Should().NotBeNull();
        }
        [Fact]
        public async Task RecurringTransactionService_Updates_Wallet_Updated_RecurringTransaction_Increase()
        {
            DateTime now = DateTime.UtcNow;

            UserModel user = new UserModel("Test User", new DateTime(), "randomemail@gmail.com", "password", api.Enums.Currency.Euro);
            user._id = "1234567891234567891234";
            user.Role = "user";

            WalletModel wallet = new WalletModel("Unit Test", 0, true, 490, null, api.Enums.WalletColor.Green, now);
            wallet._id = "1234567891234567891234";
            wallet.DateOffsetBalance = new List<DateOffsetBalance>();
            wallet.DateOffsetBalance.Add(new DateOffsetBalance(DateTime.UtcNow, 490));

            user.Wallets.Add(wallet);

            RecurringTransactionModel oldRecurringTransactionModel = new RecurringTransactionModel(user._id, wallet._id, "Netflix", 100, 27, api.Enums.TransactionCategory.Entertainment, api.Enums.RecurringType.Subscription);
            RecurringTransactionModel newRecurringTransactionModel = new RecurringTransactionModel(user._id, wallet._id, "Netflix", 110, 27, api.Enums.TransactionCategory.Entertainment, api.Enums.RecurringType.Subscription);

            RecurringTransactionService service = new RecurringTransactionService();
            WalletModel newWallet = await service.UpdateWalletForUpdatedRecurringTransaction(user, newRecurringTransactionModel, oldRecurringTransactionModel);

            newWallet.Balance.Should().Be(480);
            newWallet.LastUpdated.Should().NotBeOnOrBefore(now);
            newWallet.DateOffsetBalance.Should().NotBeNull();
        }
        [Fact]
        public async Task RecurringTransactionService_Updates_Wallet_Updated_RecurringTransaction_Decrease()
        {
            DateTime now = DateTime.UtcNow;

            UserModel user = new UserModel("Test User", new DateTime(), "randomemail@gmail.com", "password", api.Enums.Currency.Euro);
            user._id = "1234567891234567891234";
            user.Role = "user";

            WalletModel wallet = new WalletModel("Unit Test", 0, true, 490, null, api.Enums.WalletColor.Green, now);
            wallet._id = "1234567891234567891234";
            wallet.DateOffsetBalance = new List<DateOffsetBalance>();
            wallet.DateOffsetBalance.Add(new DateOffsetBalance(DateTime.UtcNow, 490));

            user.Wallets.Add(wallet);

            TransactionModel oldTransaction = new TransactionModel(user._id, wallet._id, 20, DateTime.UtcNow, api.Enums.TransactionCategory.Automotive, "test", "test", "test");
            TransactionModel newTransaction = new TransactionModel(user._id, wallet._id, 10, DateTime.UtcNow, api.Enums.TransactionCategory.Automotive, "test", "test", "test");

            TransactionService service = new TransactionService();
            WalletModel newWallet = await service.UpdateWalletForUpdatedTransaction(user, newTransaction, oldTransaction);

            newWallet.Balance.Should().Be(500);
            newWallet.LastUpdated.Should().NotBeOnOrBefore(now);
            newWallet.DateOffsetBalance.Should().NotBeNull();
        }
        [Fact]
        public async Task RecurringTransactionService_Updates_Wallet_Deleted_RecurringTransaction()
        {
            DateTime now = DateTime.UtcNow;

            UserModel user = new UserModel("Test User", new DateTime(), "randomemail@gmail.com", "password", api.Enums.Currency.Euro);
            user._id = "1234567891234567891234";
            user.Role = "user";

            WalletModel wallet = new WalletModel("Unit Test", 0, true, 100, null, api.Enums.WalletColor.Green, now);
            wallet._id = "1234567891234567891234";
            wallet.DateOffsetBalance = new List<DateOffsetBalance>();
            wallet.DateOffsetBalance.Add(new DateOffsetBalance(DateTime.UtcNow, 100));

            user.Wallets.Add(wallet);

            TransactionModel transaction = new TransactionModel(user._id, wallet._id, 100, DateTime.UtcNow, api.Enums.TransactionCategory.Automotive, "test", "test", "test");

            TransactionService service = new TransactionService();
            WalletModel newWallet = await service.UpdateWalletForDeletedTransaction(user, transaction);

            newWallet.Balance.Should().Be(200);
            newWallet.LastUpdated.Should().NotBeOnOrBefore(now);
            newWallet.DateOffsetBalance.Should().NotBeNull();
        }
    }
}
