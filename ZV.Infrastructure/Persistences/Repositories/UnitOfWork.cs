using ZV.Infrastructure.Persistences.Contexts;
using ZV.Infrastructure.Persistences.Interfaces;

namespace ZV.Infrastructure.Persistences.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataBaseContext _context;

        public IUserInfoRepository UserInfoRepository { get; private set; }
        public ICommerceRepository CommerceRepository { get; private set; }
        public ITransactionRepository TransactionRepository { get; private set; }
        public ICredentialsRepository CredentialsRepository { get; private set; }
        public IClientRepository ClientRepository { get; private set; }

        public UnitOfWork(DataBaseContext context)
        {
            _context = context;
            UserInfoRepository = new UserInfoRepository(_context);
            CommerceRepository = new CommerceRepository(_context);
            TransactionRepository = new TransactionRepository(_context);
            CredentialsRepository = new CredentialsRepository(_context);
            ClientRepository = new ClientRepository(_context);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
