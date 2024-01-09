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

namespace ZV.Infrastructure.Persistences.Repositories
{
    public class UserInfoRepository : GenericRepository<UserInfo>, IUserInfoRepository
    {
        //private readonly DataBaseContext _context;
        public async Task<BaseEntityResponse<UserInfo>> ListUserInfo(BaseFilterRequest filters)
        {

            throw new NotImplementedException();
            /* var response = new BaseEntityResponse<UserInfo>();

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
             if (filters.Sort is null) filters.Sort = "user_id";
             response.TotalRecords = await usersInfo.CountAsync();
             response.Items = await Ordering(filters, usersInfo, !(bool)filters.Download!).ToListAsync();

             return response;*/
        }
        public UserInfoRepository(DataBaseContext context) : base(context)
        {
            //_context = context;
        }

        public async Task<bool> EditUserInfo(UserInfo userinfo)
        {
            throw new NotImplementedException();
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

        public async Task<bool> RegisterMultipleUserInfo(HashSet<UserInfo> usersinfo)
        {
            //usersinfo.ToList().ForEach(x => x.UserStatus=true);
            foreach (UserInfo userInfo in usersinfo)
            {
                var existingUser = _context.UserInfos.Find(userInfo.UserId);

                if (existingUser != null)
                {
                    usersinfo.RemoveWhere(user => user.UserId == existingUser.UserId);
                }
            }

            await _context.AddRangeAsync(usersinfo);
            var recordsAffected = await _context.SaveChangesAsync();
            return recordsAffected > 0;
        }
    }
}
