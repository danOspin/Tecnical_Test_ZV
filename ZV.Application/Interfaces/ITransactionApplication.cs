using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using ZV.Application.Commons.Bases;
using ZV.Application.Dtos.Request;
using ZV.Application.Dtos.Response;
using ZV.Infrastructure.Commons.Bases.Request;
using ZV.Infrastructure.Commons.Bases.Response;

namespace ZV.Application.Interfaces
{
    public interface ITransactionApplication
    {
        Task<BaseResponse<BaseEntityResponse<TransactionResponseDto>>> ListTransaction(BaseFilterRequest filter);
        Task<BaseResponse<TransactionResponseDto>> TransactionById(string id);
        Task<BaseResponse<bool>> RegisterTransaction(TransactionRequestDto transaction);
        Task<BaseResponse<bool>> RegisterMultipleTransactions(HashSet<TransactionRequestDto> transactions);
    }
}
