using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZV.Infrastructure.Commons.Bases.Request
{
    public class TransactionsPerClientRequestDto : BaseFilterRequest
    {
        public string _client_origin_id { get; set; }
        public string _transaction_code { get; set; }
        public string _client_filter_id { get; set; }

    }
}
