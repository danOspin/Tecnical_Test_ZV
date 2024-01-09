namespace ZV.Infrastructure.Persistences.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IUserInfoRepository UserInfoRepository { get; }
        ICommerceRepository CommerceRepository { get; }
        ITransactionRepository TransactionRepository { get; }
        ICredentialsRepository CredentialsRepository { get; }
        IClientRepository ClientRepository { get; }
        void SaveChanges();
        Task SaveChangesAsync();
    }
}
