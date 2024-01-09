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
    public class ClientRepository : GenericRepository<Client>, IClientRepository
    {
        //private readonly DataBaseContext _context;
        public async Task<BaseEntityResponse<UserInfo>> ListUserInfo(BaseFilterRequest filters)
        {
            throw new NotImplementedException();
            /*var response = new BaseEntityResponse<UserInfo>();

            var usersInfo = (from c in _context.UserInfos 
                             where c.UserStatus == true
                             select c).AsNoTracking().AsQueryable();

            if (filters.NumFilter is not null && !string.IsNullOrEmpty(filters.TextFilter))
            {
                   //Numfilters
                   //1 = filtrar por id
                   //2 = filtrar por username
                 
                switch (filters.NumFilter)
                {
                    case 1:
                        usersInfo = usersInfo.Where(x => x.UserId!.Contains(filters.TextFilter));
                        break;
                    case 2:
                        usersInfo = usersInfo.Where(x => x.UserName!.Contains(filters.TextFilter));
                        break;
                }
            }
            if (filters.StateFilter is not null)
            {
                usersInfo = usersInfo.Where(x => x.UserStatus.Equals(filters.StateFilter));
            }
            
            // esto irá a filtro transacciones
            //Filtro por fecha
            //var transaction = "";
            //TODO: && Convert.ToDateTime(filters.StartDate) < Convert.ToDateTime(filters.EndDate) validación de endate > start date
            //if (!string.IsNullOrEmpty(filters.StartDate) && !string.IsNullOrEmpty(filters.EndDate) )
            //{
              //  transactions = transactions.Where(x => x.TransDate >= Convert.ToDateTime(filters.StartDate) && x.TransDate <= Convert.ToDateTime(filters.EndDate));
            //}
            //else if (!string.IsNullOrEmpty(filters.StartDate) && string.IsNullOrEmpty(filters.EndDate))
            //{

              //  transactions = transactions.Where(x => x.TransDate >= Convert.ToDateTime(filters.StartDate));
           // }
            
            if (filters.Sort is null) filters.Sort = "user_id";
            response.TotalRecords = await usersInfo.CountAsync();
            response.Items = await Ordering(filters, usersInfo, !(bool)filters.Download!).ToListAsync();

            return response;*/
        }
        public ClientRepository(DataBaseContext context) : base(context)
        {
            //_context = context;
        }

        public async Task<bool> EditUserInfo(UserInfo userinfo)
        {
            throw new NotImplementedException();
        }
        public async Task<Client> GetClientBasicInfo(string clientId)
        {
            var client = await _context.Clients!.AsNoTracking().FirstOrDefaultAsync(x => x.ClientId.Equals(clientId));
            
            return client!;
        }
        public async Task<Client> GetClientComplete(string clientId)
        {
            //var user = await _context.Clients!.AsNoTracking().FirstOrDefaultAsync(x => x.ClientId.Equals(userID));
            var client = await _context.Clients!.FirstOrDefaultAsync(x => x.ClientId.Equals(clientId));
            if (client.UserInfo is null && client.ClientType== "Pagador")
            {
                client.UserInfo = await _context.UserInfos!.FirstOrDefaultAsync(x => x.UserId.Equals(clientId));
            }
            else if (client.Commerce is null && client.ClientType == "Comercio")
            {
                client.Commerce = await _context.Commerces!.FirstOrDefaultAsync(x => x.CommerceId.Equals(clientId));
            }
            return client!;
        }
        public async Task<Client> CheckClientCredentials (string clientId)
        {
            //var user = await _context.Clients!.AsNoTracking().FirstOrDefaultAsync(x => x.ClientId.Equals(userID));
            var client = await _context.Clients!.FirstOrDefaultAsync(x => x.ClientId.Equals(clientId));
            if (client.UserInfo is null && client.ClientType == "Pagador")
            {
                client.UserInfo = await _context.UserInfos!.FirstOrDefaultAsync(x => x.UserId.Equals(clientId));
            }
            else if (client.Commerce is null && client.ClientType == "Comercio")
            {
                client.Commerce = await _context.Commerces!.FirstOrDefaultAsync(x => x.CommerceId.Equals(clientId));
            }
            return client!;
        }
        public async Task<UserInfo> GetUserInfo(string userID)
        {
            var user = await _context.UserInfos!.AsNoTracking().FirstOrDefaultAsync(x => x.UserId.Equals(userID));
            return user!;
        }
        public async Task<bool> UserExists(string userID)
        {
            var user = await _context.UserInfos!.AsNoTracking().FirstOrDefaultAsync(x => x.UserId.Equals(userID));
            
            return (user!=null);
        }
        public async Task<IEnumerable<UserInfo>> ListSelectUserInfo()
        {
            var users = await _context.UserInfos
                .Where(x => x.UserStatus.Equals(1)).AsNoTracking().ToListAsync();
            return users;
        }

        public async Task<bool> RegisterUserInfo(UserInfo userinfo)
        {
            userinfo.UserStatus = true;

            await _context.AddAsync(userinfo);
            var recordsAffected = await _context.SaveChangesAsync();
            return recordsAffected>0;
        }

        public async Task<bool> RemoveUserInfo(string userinfoID)
        {
            var user = await GetUserInfo(userinfoID);
            user.UserStatus = false;
            _context.Update(user);
            var recordsAffected = await _context.SaveChangesAsync();
            return recordsAffected > 0;
        }

        public async Task<bool> RegisterMultipleUserInfo(HashSet<Client> clients, string clientType)
        {
            //usersinfo.ToList().ForEach(x => x.UserStatus=true);
            foreach (Client client in clients)
            {
                var existingUser = _context.Clients.Find(client.ClientId);

                if (existingUser != null)
                {
                    //clients.RemoveWhere(user => user.ClientId == existingUser.ClientId);
                    clients.Remove(client);
                }
            }
            //En este punto se asigna finalmente que todos están activos y de tipo Client
            clients.ToList().ForEach(x => { x.ClientStatus = true; x.ClientType = clientType; });
            
            await _context.AddRangeAsync(clients);
            var recordsAffected = await _context.SaveChangesAsync();
            return recordsAffected > 0;
        }

        public async Task<bool> SetCredentials(ClientFilterRequest request)
        {
            TCredential creds = new TCredential();

            creds.Salt = BCrypt.Net.BCrypt.GenerateSalt();
            creds.Pass = BCrypt.Net.BCrypt.HashPassword(request.pass, creds.Salt);
            creds.CredentialId = request.clientid;
            creds.Username = request.username;

            await _context.AddAsync(creds);
            var recordsAffected = await _context.SaveChangesAsync();
            return recordsAffected > 0;
        }

        public async Task<TCredential> CheckCredsRegistry(ClientFilterRequest request)
        {
            TCredential credsRegistry = await _context.TCredentials!.AsNoTracking().FirstOrDefaultAsync(x => x.CredentialId.Equals(request.username) || x.Username.Equals(request.username));

            return credsRegistry;
        }

        public async Task<string> CheckCredentials(ClientFilterRequest request)
        {
            var credsRegistry = await CheckCredsRegistry(request);

            if (credsRegistry is null)
            {
                return ReplyMessage.MESSAGE_QUERY_EMPTY;
            }
            else
            {
                var testCreds = BCrypt.Net.BCrypt.HashPassword(request.pass, credsRegistry.Salt);

                //if (credsRegistry != null && BCrypt.Net.BCrypt.Verify(testCreds, credsRegistry.Pass))
                if (testCreds.Equals(credsRegistry.Pass))
                {
                    return ReplyMessage.MESSAGE_QUERY;
                }
                else
                {
                    return ReplyMessage.MESSAGE_FAILED;
                }
            }
        }
        //TODO: evaluar objetos de filtros
        public async Task<BaseEntityResponse<TransactionInfo>> ListTransactions(BaseFilterRequest filters)
        {
            /*var response = new BaseEntityResponse<TransactionInfo>();
            //se está trayendo todos los objetos.
            //
            var transactionInfo = (from c in _context.TransactionInfos
                             where c.CommerceId.Equals("AquiIraClientIdOCommerce")
                             select c).AsNoTracking().AsQueryable();
            
            //Evalua filtro. TODO: Definir Filtros
            if (filters.NumFilter is not null && !string.IsNullOrEmpty(filters.TextFilter))
            {
            */
            /* Numfilters
               1 = filtrar por id
               2 = filtrar por username
             */
            /*       switch (filters.NumFilter)
                   {
                       case 1:
                           usersInfo = usersInfo.Where(x => x.UserId!.Contains(filters.TextFilter));
                           break;
                       case 2:
                           usersInfo = usersInfo.Where(x => x.UserName!.Contains(filters.TextFilter));
                           break;
                   }
               }
               if (filters.StateFilter is not null)
               {
                   usersInfo = usersInfo.Where(x => x.UserStatus.Equals(filters.StateFilter));
               }*/
            /*
            // esto irá a filtro transacciones
            //Filtro por fecha
            //var transaction = "";
            //TODO: && Convert.ToDateTime(filters.StartDate) < Convert.ToDateTime(filters.EndDate) validación de endate > start date
            if (!string.IsNullOrEmpty(filters.StartDate) && !string.IsNullOrEmpty(filters.EndDate) )
            {
                transactions = transactions.Where(x => x.TransDate >= Convert.ToDateTime(filters.StartDate) && x.TransDate <= Convert.ToDateTime(filters.EndDate));
            }
            else if (!string.IsNullOrEmpty(filters.StartDate) && string.IsNullOrEmpty(filters.EndDate))
            {

                transactions = transactions.Where(x => x.TransDate >= Convert.ToDateTime(filters.StartDate));
            }
            */
            /* if (filters.Sort is null) filters.Sort = "user_id";
             response.TotalRecords = await usersInfo.CountAsync();
             response.Items = await Ordering(filters, usersInfo, !(bool)filters.Download!).ToListAsync();

             return response;
            */

            throw new NotImplementedException();
        }
    }
}
