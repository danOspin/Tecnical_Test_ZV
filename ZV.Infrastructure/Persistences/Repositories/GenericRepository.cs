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

namespace ZV.Infrastructure.Persistences.Repositories
{
    public class GenericRepository <T>: IGenericRepository<T> where T : class
    {

        protected IQueryable<TDTO> Ordering <TDTO> (BasePaginationRequest request, IQueryable<TDTO> queryable, bool pagination = false) where TDTO: class
        {
            IQueryable<TDTO> queryDTO = request.Order == "desc" ? queryable.OrderBy($"{request.Sort} descending") : queryable.OrderBy($"{request.Sort}ascending");

            if (pagination) queryDTO = queryDTO.Paginate(request);

            return queryDTO;
        }
    }
}
