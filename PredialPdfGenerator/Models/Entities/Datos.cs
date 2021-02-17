using System;
namespace PredialPdfGenerator.Models.Entities
{
    public class Datos
    {

        public string Error { get; set; }
        public string Mensaje { get; set; }
        public int Paginas { get; set; }
        //public List<Logisctic> Log { get; set; }
        public int Registros_Totales { get; set; }
        public int Pagina_actual { get; set; }

        public Datos()
        {
        }
    }
}
