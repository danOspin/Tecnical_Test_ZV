using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZV.Api.Controllers.Helper;
using Chronic;
using Chronic.Core;

namespace ZV.Application.Dtos.Request
{
    public class TransactionRequestDto
    {
        public string _trans_code { get; set; }
        public byte _trans_payment_method { get; set; }
        public short _trans_status {  get; set; }
        public decimal _trans_total {  get; set; } 
        public DateTime _trans_date {  get; set; }
        public string _trans_concept { get; set; }
        public string _commerce_code { get; set; }
        public string _user_id {  get; set; }

        static DateTime ParseDateTimeOrDefault(string dateString, string format)
        {
            var parser = new Parser();
            // Intenta convertir el string a DateTime usando Chronic
            Span parseResult = parser.Parse(dateString);
            DateTime fechaDateTime = DateTime.MinValue;
            if (parseResult != null)
            {
                // Imprime el resultado
                fechaDateTime = (DateTime)parseResult.Start;
            }

            return fechaDateTime;
        }

        public TransactionRequestDto(RawTransaction transaction)
        {
            _trans_code = transaction.Trans_codigo;
            _trans_payment_method = transaction.Trans_medio_pago;
            _trans_status = (short)transaction.Trans_estado;
            _trans_total = transaction.Trans_total;
            _trans_date = ParseDateTimeOrDefault(transaction.Trans_fecha, "dd/MM/yyyy hh:mm:ss tt");
            _trans_concept = transaction.Trans_concepto;
            _commerce_code = transaction.comercio_codigo.ToString();
            _user_id = transaction.usuario_identificacion;
        }

    }
}
