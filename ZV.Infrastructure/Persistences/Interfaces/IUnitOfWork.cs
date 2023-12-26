namespace ZV.Infrastructure.Persistences.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IUserInfoRepository UserInfo { get; }
        void SaveChanges();
        Task SaveChangesAsync();
    }
}
