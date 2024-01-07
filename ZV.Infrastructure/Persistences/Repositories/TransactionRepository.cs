using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZV.Domain.Entities;
using ZV.Infrastructure.Commons.Bases.Request;
using ZV.Infrastructure.Commons.Bases.Response;
using ZV.Infrastructure.Persistences.Contexts;
using ZV.Infrastructure.Persistences.Interfaces;
using ZV.Utilities.Static;

namespace ZV.Infrastructure.Persistences.Repositories
{
    public class TransactionRepository : GenericRepository<TransactionInfo>, ITransactionRepository
    {
        //private readonly DataBaseContext _context;

        public TransactionRepository(DataBaseContext context) : base(context)
        {
            //_context = context;
        }

        public async Task<bool> EditTransaction(TransactionInfo transaction)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TransactionInfo>> GetTransactionByCommerceId(string commerceID)
        {
            throw new NotImplementedException();
        }

        public async Task<TransactionInfo> GetTransactionByTransCode(string transCode)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TransactionInfo>> GetTransactionByUserId(string commerceID)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TransactionInfo>> ListSelectTrasaction()
        {
            throw new NotImplementedException();
        }

        public async Task<BaseEntityResponse<TransactionInfo>> ListTransaction(BaseFilterRequest filters)
        {
            throw new NotImplementedException();
        }

        public Task<BaseEntityResponse<TransactionInfo>> ListTransactionByUserId(BaseFilterRequest filters, string userid)
        {
            throw new NotImplementedException();
        }

        public Task<BaseEntityResponse<TransactionInfo>> ListTransactionsByCommerceId(BaseFilterRequest filters, string commerceid)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> RegisterMultipleTransactions(HashSet<TransactionInfo> transactions)
        {
            //usersinfo.ToList().ForEach(x => x.UserStatus=true);
            foreach (TransactionInfo transaction in transactions)
            {
                var existingTransaction = _context.TransactionInfos.Find(transaction.TransCode);

                if (existingTransaction != null)
                {
                    transactions.RemoveWhere(transaction => transaction.TransCode == existingTransaction.TransCode);
                }
            }

            await _context.AddRangeAsync(transactions);
            var recordsAffected = await _context.SaveChangesAsync();
            return recordsAffected > 0;
        }

        public async Task<bool> RegisterTransaction(TransactionInfo commerce)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> RemoveTransaction(string commerceID)
        {
            throw new NotImplementedException();
        }
    }
}
