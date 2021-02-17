using System;
namespace PredialPdfGenerator.Models.Entities
{
    public class Statement
    {
        public double impuesto_mas_adicional { get; set; }

        public double impuesto_predial { get; set; }

        public double gastos_ejecuacion { get; set; }

        public double cantidad_recargo { get; set; }

        public int anio { get; set; }

        public double porcentaje_descuento { get; set; }

        //public double descuentos { get; set; }

        public double base_gravable { get; set; }

        public int semestre { get; set; }

        public double adicional { get; set; }

        public double total_recargos { get; set; }

        public double multa { get; set; }

        public bool diferencia_construccion { get; set; }

        public double descuento_predial { get; set; }

        public double descuento_recargo { get; set; }

        public double descuento_descuento_multa { get; set; }

        public double descuento_gasto_ejecucion { get; set; }

        public double descuento_autorizado_predial { get; set; }

        public double descuento_autorizado_recargo { get; set; }

        public double descuento_autorizado_multa { get; set; }

        public double descuento_autorizado_gasto_ejecucion { get; set; }
    }
}
