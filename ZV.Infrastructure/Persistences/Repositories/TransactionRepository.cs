using Microsoft.EntityFrameworkCore;
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
        private ClientRepository clientRepo;
        public TransactionRepository(DataBaseContext context) : base(context)
        {
            //_context = context;
            clientRepo = new ClientRepository(context);
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

        //Este va a funcionar para comercios primero. Luego se debe hacer lo mismo para usuarios.
        public async Task<BaseEntityResponse<TransactionInfo>> ListTransaction(TransactionsPerClientRequestDto filters)
        {
            var response = new BaseEntityResponse<TransactionInfo>();
            //Cambiar este select all
            IQueryable<TransactionInfo> transactionsPerClient;

            var existingClient = await clientRepo.GetClientBasicInfo(filters._client_origin_id);
            
            if (string.IsNullOrEmpty(existingClient.ClientId))
            {
                response.CountRegister = 0;
                response.TotalTransactionsSum = 0;
                response.Items = new List<TransactionInfo>() { new TransactionInfo() };

                return response;
            }
            else if (existingClient.ClientType.Equals("Comercio"))
            {
                transactionsPerClient = (from c in _context.TransactionInfos
                                       where c.CommerceId
                                       .Equals(filters._client_origin_id)
                                       select c).AsNoTracking().AsQueryable();
            }
            else
            {
                transactionsPerClient = (from c in _context.TransactionInfos
                                         where c.UserId.Equals(filters._client_origin_id)
                                         select c).AsNoTracking().AsQueryable();
            }
            if (!string.IsNullOrEmpty(filters._transaction_code))
            {
                int trans_code = int.Parse(filters._transaction_code);
                transactionsPerClient = transactionsPerClient.Where(x => x.TransCode == trans_code);                 
            }
            if (!string.IsNullOrEmpty(filters._start_date))
            {
                DateTime start_date_converted = Helper.ParseDateTimeOrDefault(filters._start_date);
                transactionsPerClient = transactionsPerClient.Where(x => x.TransDate >= start_date_converted);
            }
            if (!string.IsNullOrEmpty(filters._end_date))
            {
                DateTime end_date_converted = Helper.ParseDateTimeOrDefault(filters._end_date);
                transactionsPerClient = transactionsPerClient.Where(x => x.TransDate <= end_date_converted);
            }
            //Filtro por usuarios.
            if (!string.IsNullOrEmpty(filters._client_filter_id))
            {
                if (existingClient.ClientType.Equals("Comercio"))
                    transactionsPerClient = transactionsPerClient.Where(x => x.UserId.Equals(filters._client_filter_id));
                else
                    transactionsPerClient = transactionsPerClient.Where(x => x.CommerceId.Equals(filters._client_filter_id)); 
            }

            //if (filters.Sort is null) filters.Sort = "TransCode";
            response.CountRegister = await transactionsPerClient.CountAsync();
            //response.Items = await Ordering(filters, transactionsPerClient, false).ToListAsync(); //!(bool)filters.Download!
            response.TotalTransactionsSum = transactionsPerClient.Sum(x => x.TransTotal);
            response.Items = await transactionsPerClient.ToListAsync();
            return response;

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
