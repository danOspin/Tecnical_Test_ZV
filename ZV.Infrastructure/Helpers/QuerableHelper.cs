using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZV.Infrastructure.Commons.Bases.Request;

namespace ZV.Infrastructure.Helpers
{
    public static class QuerableHelper
    {
        public static IQueryable<T> Paginate <T>(this IQueryable<T> queryable, BasePaginationRequest request)
        {
            return queryable.Skip((request.NumPage - 1) * request.Records).Take(request.Records);
        }
    }
}
