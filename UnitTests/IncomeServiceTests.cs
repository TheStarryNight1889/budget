using api.Helpers;
using api.Models;
using api.Services;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests
{
    public class IncomeServiceTests
    {
        //Updating Wallet Object 
        [Fact]
        public async Task IncomeService_Updates_Wallet_New_Income()
        {
            DateTime now = DateTime.UtcNow;

            UserModel user = new UserModel("Test User", new DateTime(), "randomemail@gmail.com", "password", api.Enums.Currency.Euro);
            user._id = "1234567891234567891234";
            user.Role = "user";

            WalletModel wallet = new WalletModel("Unit Test", 0, true, 100, null, api.Enums.WalletColor.Green, now);
            wallet._id = "1234567891234567891234";
            wallet.DateOffsetBalance = new List<DateOffsetBalance>();

            user.Wallets.Add(wallet);

            IncomeModel income = new IncomeModel(user._id, wallet._id, 100, DateTime.UtcNow, "unit test");

            IncomeService service = new IncomeService();
            WalletModel newWallet = await service.UpdateWalletForNewIncome(user,income);
            newWallet.Balance.Should().Be(200);
            newWallet.LastUpdated.Should().NotBeOnOrBefore(now);
            newWallet.DateOffsetBalance.Should().NotBeNull();
        }
        [Fact]
        public async Task IncomeService_Updates_Wallet_Updated_Income_Decrease()
        {
            DateTime now = DateTime.UtcNow;

            UserModel user = new UserModel("Test User", new DateTime(), "randomemail@gmail.com", "password", api.Enums.Currency.Euro);
            user._id = "1234567891234567891234";
            user.Role = "user";

            WalletModel wallet = new WalletModel("Unit Test", 0, true, 120, null, api.Enums.WalletColor.Green, now);
            wallet._id = "1234567891234567891234";
            wallet.DateOffsetBalance = new List<DateOffsetBalance>();
            wallet.DateOffsetBalance.Add(new DateOffsetBalance(DateTime.UtcNow, 120));

            user.Wallets.Add(wallet);

            IncomeModel oldIncome = new IncomeModel(user._id, wallet._id, 120, DateTime.UtcNow, "unit test");
            IncomeModel newIncome = new IncomeModel(user._id, wallet._id, 100, DateTime.UtcNow, "unit test");

            IncomeService service = new IncomeService();
            WalletModel newWallet = await service.UpdateWalletForUpdatedIncome(user, newIncome, oldIncome);

            newWallet.Balance.Should().Be(100);
            newWallet.LastUpdated.Should().NotBeOnOrBefore(now);
            newWallet.DateOffsetBalance.Should().NotBeNull();
        }
        [Fact]
        public async Task IncomeService_Updates_Wallet_Updated_Income_Increase()
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

            IncomeModel oldIncome = new IncomeModel(user._id, wallet._id, 100, DateTime.UtcNow, "unit test");
            IncomeModel newIncome = new IncomeModel(user._id, wallet._id, 120, DateTime.UtcNow, "unit test");

            IncomeService service = new IncomeService();
            WalletModel newWallet = await service.UpdateWalletForUpdatedIncome(user, newIncome, oldIncome);

            newWallet.Balance.Should().Be(120);
            newWallet.LastUpdated.Should().NotBeOnOrBefore(now);
            newWallet.DateOffsetBalance.Should().NotBeNull();
        }
        [Fact]
        public async Task IncomeService_Updates_Wallet_Deleted_Income()
        {
            DateTime now = DateTime.UtcNow;

            UserModel user = new UserModel("Test User", new DateTime(), "randomemail@gmail.com", "password", api.Enums.Currency.Euro);
            user._id = "1234567891234567891234";
            user.Role = "user";

            WalletModel wallet = new WalletModel("Unit Test", 0, true, 120, null, api.Enums.WalletColor.Green, now);
            wallet._id = "1234567891234567891234";
            wallet.DateOffsetBalance = new List<DateOffsetBalance>();
            wallet.DateOffsetBalance.Add(new DateOffsetBalance(DateTime.UtcNow, 120));

            user.Wallets.Add(wallet);

            IncomeModel oldIncome = new IncomeModel(user._id, wallet._id, 120, DateTime.UtcNow, "unit test");

            IncomeService service = new IncomeService();
            WalletModel newWallet = await service.UpdateWalletForDeletedIncome(user,oldIncome);

            newWallet.Balance.Should().Be(0);
            newWallet.LastUpdated.Should().NotBeOnOrBefore(now);
            newWallet.DateOffsetBalance.Should().NotBeNull();
        }
    }
}
