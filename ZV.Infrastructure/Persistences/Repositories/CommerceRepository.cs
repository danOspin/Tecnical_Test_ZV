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
    public class CommerceRepository : GenericRepository<Commerce>, ICommerceRepository
    {
        //private readonly DataBaseContext _context;

        public CommerceRepository(DataBaseContext context) : base(context)
        {
            //_context = context;
        }

        public Task<bool> EditCommerce(Commerce commerce)
        {
            throw new NotImplementedException();
        }

        public async Task<Commerce> GetCommerceById(string commerceID)
        {
            var commerce = await _context.Commerces!.AsNoTracking().FirstOrDefaultAsync(x => x.CommerceId.Equals(commerceID));
            return commerce!;
        }
        public async Task<Commerce> GetCommerceByNit(string commerceNit)
        {
            var commerce = await _context.Commerces!.AsNoTracking().FirstOrDefaultAsync(x => x.Nit.Equals(commerceNit));
            return commerce!;
        }

        public async Task<BaseEntityResponse<Commerce>> ListCommerce(BaseFilterRequest filters)
        {
            var response = new BaseEntityResponse<Commerce>();

            var commerces = (from c in _context.Commerces
                             select c).AsNoTracking().AsQueryable();

            if (filters.NumFilter is not null && !string.IsNullOrEmpty(filters.TextFilter))
            {
                /* Numfilters
                   1 = filtrar por commerce_code
                   2 = filtrar por nit
                 */
                switch (filters.NumFilter)
                {
                    case 1:
                        commerces = commerces.Where(x => x.CommerceId!.Equals(filters.TextFilter));
                        break;
                    case 2:
                        commerces = commerces.Where(x => x.Nit!.Contains(filters.TextFilter));
                        break;
                }
            }
            if (filters.Sort is null) filters.Sort = "user_id";

            response.TotalRecords = await commerces.CountAsync();
            response.Items = await Ordering(filters, commerces, !(bool)filters.Download!).ToListAsync();

            return response;
        }

        public async Task<IEnumerable<Commerce>> ListSelectCommerce()
        {
            var commerce = await _context.Commerces.AsNoTracking().ToListAsync();
            return commerce;
        }

        public async Task<bool> RegisterCommerce(Commerce commerce)
        {
            await _context.AddAsync(commerce);
            var recordsAffected = await _context.SaveChangesAsync();
            return recordsAffected > 0;
        }

        public async Task<bool> RegisterMultipleCommerces(HashSet<Commerce> commerces)
        {
            //usersinfo.ToList().ForEach(x => x.UserStatus=true);
            foreach (Commerce commerce in commerces)
            {
                var existingUser = _context.Commerces.Find(commerce.CommerceId);

                if (existingUser != null)
                {
                    commerces.RemoveWhere(commerce => commerce.CommerceId == existingUser.CommerceId);
                }
            }

            await _context.AddRangeAsync(commerces);
            var recordsAffected = await _context.SaveChangesAsync();
            return recordsAffected > 0;
        }

        public Task<bool> RemoveCommerce(string commerceID)
        {
            throw new NotImplementedException();
        }
    }
}
