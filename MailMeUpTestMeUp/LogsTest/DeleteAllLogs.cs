using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MailMeUpTestMeUp.LogsTest
{
    public class DeleteAllLogs : SuperUnitTest
    {
        [Theory]
        [InlineData("Boss", "Boss", true, true)]
        [InlineData("", "", false, true)]
        [InlineData("BossWrong", "BossWrong", false, true)]
        public async Task DeleteAllLogsTest(string username, string password, bool correctCreds, bool succesfulRequest)
        {
            var token = await Login(username, password);
            Assert.Equal(correctCreds, !string.IsNullOrEmpty(token));
            if (!string.IsNullOrEmpty(token))
            {
                var response = await _lib.Log.DeleteAllLogs(token);
                Assert.NotNull(response);
                Assert.Equal(succesfulRequest, response.StatusCode == System.Net.HttpStatusCode.OK);
                Assert.Equal(succesfulRequest, response.Success);
                Assert.Equal(succesfulRequest, string.IsNullOrWhiteSpace(response.ErrorMessage));
                if (response.Success)
                {
                    Assert.NotNull(response.Data); 
                    Assert.Equal(correctCreds, response.Data.Success);
                    Assert.Equal(correctCreds, string.IsNullOrWhiteSpace(response.Data.ErrorMessage)); 
                }
                var logoutResult = await Logout(token);
                Assert.Equal(!string.IsNullOrEmpty(token), logoutResult);
            }
        }
    }
}
