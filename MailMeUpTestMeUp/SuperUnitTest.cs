using MailMeUpLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MailMeUpTestMeUp
{
    public class SuperUnitTest
    {
        protected readonly MailMeUp _lib;

        public SuperUnitTest()
        {
            _lib = new MailMeUp("https://localhost:7101");
        }

        protected async Task<bool> Logout(string token)
        {
            try
            {
                var logoutResult = await _lib.User.Logout(token);
                var tokenCheck = string.IsNullOrEmpty(token);
                Assert.NotNull(logoutResult);
                Assert.Equal(!tokenCheck, logoutResult.StatusCode == System.Net.HttpStatusCode.OK);
                Assert.Equal(!tokenCheck, logoutResult.Success);
                Assert.Equal(!tokenCheck, string.IsNullOrWhiteSpace(logoutResult.ErrorMessage));
                Assert.NotNull(logoutResult.Data);
                Assert.Equal(!tokenCheck, logoutResult.Data.Success);
                Assert.Equal(!tokenCheck, string.IsNullOrWhiteSpace(logoutResult.Data.ErrorMessage));
                Assert.Equal(!tokenCheck, string.IsNullOrEmpty(logoutResult.Data.ErrorMessage));
                return true;
            }
            catch
            {
                return false;
            }

        }

        protected async Task<string> Login(string username, string password)
        {
            try
            {
                var result = await _lib.User.Login(new Dtos.LoginDto() { Username = username, Password = password });
                return result.Data.Token;
            }
            catch
            {
                return null;
            }
        }
    }
}
