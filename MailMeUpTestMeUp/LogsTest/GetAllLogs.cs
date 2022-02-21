using MailMeUpLib;
using Models.Responses;
using System;
using System.Threading.Tasks;
using Xunit;

namespace MailMeUpTestMeUp
{
    public class GetAllLogs : SuperUnitTest
    {   
        [Theory]
        [InlineData("Boss", "Boss", true, true)]
        [InlineData("", "", false, true)]
        [InlineData("BossWrong", "BossWrong", false, true)]
        public async Task GetAllLogsTest(string username, string password, bool correctCreds, bool succesfulRequest)
        {
            var token = await Login(username, password);
            Assert.Equal(correctCreds, !string.IsNullOrEmpty(token));
            if (!string.IsNullOrEmpty(token))
            {
                var response = await _lib.Log.GetAlllogs(token);
                Assert.NotNull(response);
                Assert.Equal(succesfulRequest, response.StatusCode == System.Net.HttpStatusCode.OK);
                Assert.Equal(succesfulRequest, response.Success);
                Assert.Equal(succesfulRequest, string.IsNullOrWhiteSpace(response.ErrorMessage));
                if (response.Success)
                {
                    Assert.NotNull(response.Data);
                    Assert.Equal(correctCreds, response.Data.Logs is not null);
                    Assert.Equal(correctCreds, response.Data.Success);
                    Assert.Equal(correctCreds, string.IsNullOrWhiteSpace(response.Data.ErrorMessage));
                    var logoutResult = await Logout(token);
                    Assert.Equal(!string.IsNullOrEmpty(token), logoutResult);
                }
            }
        } 
    }
}