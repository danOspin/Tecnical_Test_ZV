using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZV.Application.Dtos.Response
{
    public class TransactionResponseDto
    {
        public string Transaction_code { get; set; }
        public byte Transaction_payment_method { get; set; }
        public short Transaction_status { get; set; }
        public decimal Transaction_total { get; set; }
        public string Transaction_date { get; set; }
        public string Transaction_concept { get; set; }
        public string Commerce_code { get; set; }
        public string User_id { get; set; }
    }
}
