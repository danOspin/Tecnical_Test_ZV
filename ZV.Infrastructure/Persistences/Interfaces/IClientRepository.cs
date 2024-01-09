using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZV.Domain.Entities;
using ZV.Infrastructure.Commons.Bases.Request;
using ZV.Infrastructure.Commons.Bases.Response;

namespace ZV.Infrastructure.Persistences.Interfaces
{
    public interface IClientRepository
    {
        Task<BaseEntityResponse<UserInfo>> ListUserInfo(BaseFilterRequest filters);
        Task<IEnumerable<UserInfo>> ListSelectUserInfo();
        Task<UserInfo> GetUserInfo(string userID);
        Task<bool> RegisterUserInfo(UserInfo userinfo);
        Task<bool> RegisterMultipleUserInfo(HashSet<Client> usersinfo, string clientType);

        Task<bool> EditUserInfo (UserInfo userinfo);
        Task<bool> RemoveUserInfo(string userinfoID);
        Task<Client> GetClientBasicInfo(string userid);
        Task<Client> GetClientComplete(string userid);
        Task<bool> SetCredentials(ClientFilterRequest request);
        Task<string> CheckCredentials(ClientFilterRequest request);
        Task<TCredential> CheckCredsRegistry(ClientFilterRequest request);

    }
}
