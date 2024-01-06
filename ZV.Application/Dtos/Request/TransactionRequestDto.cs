using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZV.Api.Controllers.Helper;

namespace ZV.Application.Dtos.Request
{
    public class TransactionRequestDto
    {
        public string _trans_code { get; set; }
        public byte _trans_payment_method { get; set; }
        public byte _trans_status {  get; set; }
        public decimal _trans_total {  get; set; } 
        public DateTime _trans_date {  get; set; }
        public string _trans_concept { get; set; }
        public string _commerce_code { get; set; }
        public string _user_id {  get; set; }

        static DateTime ParseDateTimeOrDefault(string dateString, string format)
        {
            if (DateTime.TryParseExact(dateString, format, CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out DateTime result))
            {
                return result;
            }
            return DateTime.MinValue;
        }

        public TransactionRequestDto(RawTransaction transaction)
        {
            _trans_code = transaction.Trans_codigo;
            _trans_payment_method = transaction.Trans_medio_pago;
            _trans_status = (byte)transaction.Trans_estado;
            _trans_total = transaction.Trans_total;
            _trans_date = ParseDateTimeOrDefault(transaction.Trans_fecha, "d/M/yyyy h:mm:ss tt");
            _trans_concept = transaction.Trans_concepto;
            _commerce_code = transaction.comercio_codigo.ToString();
            _user_id = transaction.usuario_identificacion;
        }

    }
}
