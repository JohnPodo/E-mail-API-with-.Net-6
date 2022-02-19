using MailMeUpLib;
using System.Threading.Tasks;
using Xunit;

namespace MailMeUpTestMeUp
{
    public class UnitTest1
    {
        private readonly MailMeUp _lib;

        public UnitTest1()
        {
            _lib = new MailMeUp("https://localhost:7101");
        }
        [Theory]
        [InlineData("Boss", "Boss", true)]
        [InlineData("", "", false)]
        [InlineData("BossWrong", "BossWrong", false)]
        public async Task LoginAndLogout(string username, string password, bool correctCreds)
        {
            var result = await _lib.User.Login(new Dtos.LoginDto() { Username = username, Password = password });
            if (correctCreds)
            {
                Assert.True(result.StatusCode == System.Net.HttpStatusCode.OK);
                Assert.Equal(correctCreds, result.Success);
                Assert.True(string.IsNullOrWhiteSpace(result.ErrorMessage));
                Assert.True(result.Data is not null);
                Assert.True(!string.IsNullOrWhiteSpace(result.Data.Token));
                Assert.True(string.IsNullOrWhiteSpace(result.Data.ErrorMessage));
                Assert.Equal(correctCreds,result.Data.Success); 
            } 
        }
    }
}