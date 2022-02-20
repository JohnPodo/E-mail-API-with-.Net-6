using MailMeUpLib;
using Models.Responses;
using System;
using System.Threading.Tasks;
using Xunit;

namespace MailMeUpTestMeUp
{ 
    public class LoginLogout : SuperUnitTest
    {  
        [Theory]
        [InlineData("Boss", "Boss", true, true)]
        [InlineData("", "", false, true)]
        [InlineData("BossWrong", "BossWrong", false, true)]
        public async Task LoginAndLogout(string username, string password, bool correctCreds, bool succesfulRequest)
        {
            var result = await _lib.User.Login(new Dtos.LoginDto() { Username = username, Password = password }); 
            Assert.NotNull(result);
            Assert.Equal(succesfulRequest, result.StatusCode == System.Net.HttpStatusCode.OK);
            Assert.Equal(succesfulRequest, result.Success); 
            Assert.Equal(succesfulRequest, string.IsNullOrWhiteSpace(result.ErrorMessage));
            if (result.Success)
            {
                Assert.NotNull(result.Data);
                Assert.Equal(correctCreds, !string.IsNullOrWhiteSpace(result.Data.Token));
                Assert.Equal(correctCreds, string.IsNullOrWhiteSpace(result.Data.ErrorMessage));
                var logoutResult = await Logout(result.Data.Token);
                Assert.Equal(correctCreds, logoutResult);
            } 
        } 
    }
}