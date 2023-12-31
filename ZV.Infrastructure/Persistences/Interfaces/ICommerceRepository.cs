﻿using ZV.Domain.Entities;
using ZV.Infrastructure.Commons.Bases.Request;
using ZV.Infrastructure.Commons.Bases.Response;

namespace ZV.Infrastructure.Persistences.Interfaces
{
    public interface ICommerceRepository
    {
        Task<BaseEntityResponse<Commerce>> ListCommerce(BaseFilterRequest filters);
        Task<IEnumerable<Commerce>> ListSelectCommerce();
        Task<Commerce> GetCommerceById(string commerceID);
        Task<bool> RegisterCommerce(Commerce commerce); 
        Task<bool> RegisterMultipleCommerces(HashSet<Commerce> commerces);
        Task<bool> EditCommerce(Commerce commerce);
        //Task<bool> RemoveCommerce(string commerceID);
    }
}
