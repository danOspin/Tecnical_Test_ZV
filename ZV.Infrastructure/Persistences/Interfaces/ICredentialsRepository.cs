using ZV.Domain.Entities;

namespace ZV.Infrastructure.Persistences.Interfaces
{
    public interface ICredentialsRepository
    {
        Task<bool> RegisterUserCredential(TCredential credentials);
        Task<bool> EditCredentials(TCredential credential);
    }
}
