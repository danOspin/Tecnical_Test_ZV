using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ZV.Application.Interfaces;
using ZV.Application.Services;
using ZV.Utilities.Static;

namespace ZV.Test.UserInfo
{
    [TestClass]
    public class UserInfoApplicationTest
    {
        private static WebApplicationFactory<Program>? _factory = null;
        private static IServiceScopeFactory? _scopeFactory = null;

        [ClassInitialize]
        public static void Initialize (TestContext _testcontext)
        {
            _factory = new CustomWebApplicationFactory();
            _scopeFactory = _factory.Services.GetService<IServiceScopeFactory>();

        }

        [TestMethod]
        public async Task RegisterUserInfo_WhenSendingNullValuesOrEmpty_ValidationErrors()
        {
            using var scope = _scopeFactory?.CreateScope();
            var context = scope?.ServiceProvider.GetService<ITransactionApplication>();

            var name = "";

            var expected = ReplyMessage.MESSAGE_VALIDATE;

            
            
        }

    }
}
