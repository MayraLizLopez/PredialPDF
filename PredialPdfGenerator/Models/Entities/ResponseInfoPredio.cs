using System;
using System.Collections.Generic;

namespace PredialPdfGenerator.Models.Entities
{
    public class ResponseInfoPredio
    {
        public List<Citizen> datos { get; set; }

        public int registros_totales { get; set; }
        public int paginas { get; set; }
        public int pagina_actual { get; set; }

        public string error { get; set; }

        public string mensaje { get; set; }

        public bool excepcion { get; set; }



        public ErrorDescription error_descrip { get; set; }
    }
}
