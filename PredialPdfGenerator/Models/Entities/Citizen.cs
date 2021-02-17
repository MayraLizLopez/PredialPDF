using System;
using System.Collections.Generic;

namespace PredialPdfGenerator.Models.Entities
{
    public class Citizen
    {
        public int numero { get; set; }
        public string domicilio { get; set; }
        public List<PropeKiosko> propietarios { get; set; }
        public string cve_cat_ori { get; set; }
        public string cve_cat_est { get; set; }
        public double valor_terreno { get; set; }
        public double valor_construccion { get; set; }

        public Citizen(List<PropeKiosko> propietarios)
        {
            this.propietarios = propietarios;

        }

    }

    public class PropeKiosko
    {
        public string tipo_persona { get; set; }
        public string nombre { get; set; }
        public string apellido_paterno { get; set; }
        public string apellido_materno { get; set; }
        public bool titular { get; set; }

    }
}
