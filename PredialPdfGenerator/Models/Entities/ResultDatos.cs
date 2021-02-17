using System;
using System.Collections.Generic;

namespace PredialPdfGenerator.Models.Entities
{
    public class ResultDatos
    {
        public string Cve_cat_est { get; set; }
        public List<Propietarios> propietarios { get; set; }
        public string Cve_cat_ori { get; set; }
        public string Valor_construccion { get; set; }
        public string Domicilio { get; set; }
        public string Valor_terreno { get; set; }
        public int Numero { get; set; }

        public ResultDatos(List<Propietarios> propietarios)
        {
            this.propietarios = propietarios;
        }
    }
}
