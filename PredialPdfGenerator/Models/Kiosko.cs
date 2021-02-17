using System;
namespace PredialPdfGenerator.Models
{
    public class Kiosko
    {
        public string Nombre { get; set; }
        public string Contribuyente { get; set; }
        public string Anio { get; set; }
        public string Semestres { get; set; }
        public string Domicilio { get; set; }
        public string ValorCatastral { get; set; }
        public string ValorTerreno { get; set; }
        public string ValorConstruccion { get; set; }
        public string ClaveCatastral { get; set; }
        public string ClaveCatastralx { get; set; }
        public string Corriente { get; set; }
        public string Adicional { get; set; }
        public string Recargos { get; set; }
        public string Rezagos { get; set; }
        public string AdicionalRezago { get; set; }
        public string RecargosRezago { get; set; }
        public string Multa { get; set; }
        public string Honorario { get; set; }
        public string Ejecucion { get; set; }
        public string Descuento { get; set; }
        public string DiferenciaConstruccion { get; set; }
        public string Total { get; set; }
        public int TotalSinFormato { get; set; }
        public string Fecha { get; set; }
        public string ReferenciaOxxo { get; set; }
        public string LineaCaptura { get; set; }
        public string FechaVencimiento { get; set; }
        public string FechaVencimientoTxt { get; set; }

    }
}
