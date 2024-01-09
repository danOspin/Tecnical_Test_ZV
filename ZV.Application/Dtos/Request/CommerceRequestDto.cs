using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZV.Api.Controllers.Helper;
using ZV.Infrastructure.Commons.Bases.Request;

namespace ZV.Application.Dtos.Request
{
    public class CommerceRequestDto 
    {
        public string _commerce_code {  get; set; }
        public string _commerce_name { get; set; }
        public string _commerce_nit { get; set; }
        public string _commerce_address {  get; set; }

        public bool _commerce_status { get; set; }
        public CommerceRequestDto(RawTransaction transaction) 
        {
            _commerce_code = transaction.comercio_codigo.ToString();
            _commerce_name = transaction.comercio_nombre;
            _commerce_nit = transaction.comercio_nit;
            _commerce_address = transaction.comercio_direccion;
            _commerce_status = true;
        }
        public CommerceRequestDto(string comercio_codigo, string comercio_nombre, string comercio_nit, string comercio_direccion, bool commerce_status)
        {
            _commerce_code = comercio_codigo;
            _commerce_name = comercio_nombre;
            _commerce_nit = comercio_nit;
            _commerce_address = comercio_direccion;
            _commerce_status = commerce_status;
        }
        /// <summary>
        /// Se sobreescriben los métodos equals y getHashCode, para garantizar un comportamiento correcto
        /// a la hora de compararse al ser agregados en el hashset del api y así evitar elementos duplicados
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object? obj)
        {
            if (obj is CommerceRequestDto commerce)
            {
                return _commerce_code == commerce._commerce_code && _commerce_nit == commerce._commerce_nit;
            }
            return false;
        }
        public override int GetHashCode()
        {
            return _commerce_nit.GetHashCode() ^ _commerce_code.GetHashCode();
        }
    }
}
