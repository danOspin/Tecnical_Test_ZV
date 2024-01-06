using ZV.Domain.Entities;

namespace ZV.Infrastructure.Persistences.Interfaces
{
    public interface ICredentialsRepository
    {
        Task<bool> RegisterUserCredential(UserCredential credentials);
        Task<bool> EditCredentials(UserCredential credential);
    }
}
