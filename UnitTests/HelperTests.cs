using api.Helpers;
using api.Models;
using FluentAssertions;
using System;
using Xunit;

namespace UnitTests
{
    public class HelperTests
    {
        // Token Service
        [Fact]
        public void TokenService_CreateToken_Returns_Token()
        {
            UserModel user = new UserModel("Test User", new DateTime(), "randomemail@gmail.com", "password", api.Enums.Currency.Euro);
            user._id = "1234567891234567891234";
            user.Role = "user";
            string token = TokenService.CreateToken(user).ToString();
            token.Should().NotBeNull();
        }
    }
}
