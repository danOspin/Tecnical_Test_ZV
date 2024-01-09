using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ZV.Application.Interfaces;
using ZV.Infrastructure.Commons.Bases.Request;

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
        public async Task ListTransaction_WhenSendingNoFilters_ValidateTransactionsCount()
        {
            using var scope = _scopeFactory?.CreateScope();
            var context = scope?.ServiceProvider.GetService<ITransactionApplication>();

            var name = "";

            string test_client_origin_id = "3129";
            string test_start_date = "13/05/2020";
            string test_end_date = "13/05/2024";
            string test_transaction_code = "";
            string test_client_filter_id = "";

            var expected = 2;

            var result = await context.ListTransaction(new TransactionsPerClientRequestDto()
            {
                _client_origin_id = test_client_origin_id,
                _start_date = test_start_date,
                _end_date = test_end_date,
                _transaction_code = test_transaction_code,
                _client_filter_id = test_client_filter_id
            });

            
        }

    }
}
