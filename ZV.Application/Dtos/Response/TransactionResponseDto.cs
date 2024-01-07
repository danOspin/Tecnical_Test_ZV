using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZV.Application.Dtos.Response
{
    public class TransactionResponseDto
    {
        public string _trans_code { get; set; }
        public byte _trans_payment_method { get; set; }
        public short _trans_status { get; set; }
        public decimal _trans_total { get; set; }
        public string _trans_date { get; set; }
        public string _trans_concept { get; set; }
        public string _commerce_code { get; set; }
        public string _user_id { get; set; }
    }
}
