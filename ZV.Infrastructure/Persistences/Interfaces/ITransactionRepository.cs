using ZV.Domain.Entities;
using ZV.Infrastructure.Commons.Bases.Request;
using ZV.Infrastructure.Commons.Bases.Response;

namespace ZV.Infrastructure.Persistences.Interfaces
{
    internal interface ITransactionRepository
    {
        //Existen varias formas de traer resultados: Filtro por userID, commerceID, Fechas, estadoPago

        Task<BaseEntityResponse<TransactionInfo>> ListTransaction(BaseFilterRequest filters);
        Task<IEnumerable<TransactionInfo>> ListSelectTrasaction();
        Task<TransactionInfo> GetTransactionByTransCode(string transCode);
        //Task<TransactionInfo> GetTransactionByCommerceId(string commerceID);
        //Task<TransactionInfo> GetTransactionByUserId(string commerceID);
        Task<bool> RegisterTransaction(TransactionInfo commerce);
        Task<bool> EditTransaction(TransactionInfo transaction);
        Task<bool> RemoveTransaction(string commerceID);
    }
}

//Error en 0x8007001f

