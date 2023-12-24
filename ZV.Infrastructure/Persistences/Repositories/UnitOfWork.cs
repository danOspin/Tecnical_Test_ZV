using ZV.Infrastructure.Persistences.Contexts;
using ZV.Infrastructure.Persistences.Interfaces;

namespace ZV.Infrastructure.Persistences.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataBaseContext _context;

        public IUserInfoRepository UserInfo { get; private set; }

        public UnitOfWork(DataBaseContext context)
        {
            _context = context;
            UserInfo = new UserInfoRepository(_context);
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
