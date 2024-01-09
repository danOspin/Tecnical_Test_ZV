using Chronic.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZV.Utilities.Static
{
    public static class Helper
    { 
    public static DateTime ParseDateTimeOrDefault(string dateString)
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
    }
}
