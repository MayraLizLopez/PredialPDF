using System;
using System.Collections.Generic;

namespace PredialPdfGenerator.Models
{
    public class ResponsePredial
    {
        public string mensaje { get; set; }

        public bool excepcion { get; set; }

        public bool error { get; set; }

        public double total_recargos { get; set; }

        public List<Datos> datos { get; set; }
    }
}
