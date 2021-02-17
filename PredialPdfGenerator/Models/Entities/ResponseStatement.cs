using System;
using System.Collections.Generic;

namespace PredialPdfGenerator.Models.Entities
{
    public class ResponseStatement
    {
        public string mensaje { get; set; }

        public List<Statement> datos { get; set; }
        public string error { get; set; }
        public bool excepcion { get; set; }
        public double total_recargos { get; set; }

        public ErrorDescription error_descrip { get; set; }

        public string domicilio { get; set; }
        public string cve_cat_ori { get; set; }
        public string cve_cat_est { get; set; }
        public double valor_terreno { get; set; }
        public double valor_construccion { get; set; }

        public double valor_catastral { get; set; }

        public string tipo_persona { get; set; }
        public string nombre { get; set; }
        public string apellido_paterno { get; set; }
        public string apellido_materno { get; set; }

        public string contribuyente { get; set; }


        public ResponseStatement()
        {

        }

        public ResponseStatement(List<Statement> _datos)
        {
            this.datos = _datos;

        }
        public ResponseStatement(ErrorDescription error_descrip)
        {
            this.error_descrip = error_descrip;
        }
    }

    public class ErrorDescription
    {
        public bool error { get; set; }

        public string mensaje { get; set; }

        public bool excepcion { get; set; }
    }
}
