using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZV.Infrastructure.Persistences.Interfaces;
using System.Linq.Dynamic.Core;
using ZV.Infrastructure.Helpers;
using ZV.Infrastructure.Commons.Bases.Request;
using ZV.Infrastructure.Persistences.Contexts;
using Microsoft.EntityFrameworkCore;
using ZV.Domain.Entities;

namespace ZV.Infrastructure.Persistences.Repositories
{
    public class GenericRepository <T>: IGenericRepository<T> where T : class//BaseEntity
    {

        public readonly DataBaseContext _context;
        private readonly DbSet<T> _entity;
        public GenericRepository(DataBaseContext context)
        {
            _context = context;
            _entity = _context.Set<T>();
        }
        protected IQueryable<TDTO> Ordering <TDTO> (BasePaginationRequest request, IQueryable<TDTO> queryable, bool pagination = false) where TDTO: class
        {
            //IQueryable<TDTO> queryDTO = request.GetOrder() == "desc" ? queryable.OrderBy($"{request.Sort} descending") : queryable.OrderBy($"{request.Sort}ascending");

            //if (pagination) queryDTO = queryDTO.Paginate(request);

            return null;
        }
        /*public async Task<bool> RegisterMultipleContent(HashSet<T> hashSetContentDto) 
        {
            //usersinfo.ToList().ForEach(x => x.UserStatus=true);
            foreach (T contentDto in hashSetContentDto)
            {
                var existingDto = _entity.Find(contentDto.);

                if (existingDto != null)
                {
                    usersinfo.RemoveWhere(user => user.UserId == existingUser.UserId);
                }
            }

            await _context.AddRangeAsync(usersinfo);
            var recordsAffected = await _context.SaveChangesAsync();
            return recordsAffected > 0;
        }*/
    }
}
