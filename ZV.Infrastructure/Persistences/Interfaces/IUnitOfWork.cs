using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZV.Infrastructure.Persistences.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IUserInfoRepository UserInfo { get; }
        void SaveChanges();
        Task SaveChangesAsync();
    }
}
