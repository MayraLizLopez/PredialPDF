using System;
namespace PredialPdfGenerator.Models.Entities
{
    public class ResultKiosko
    {
        public string Error { get; set; }
        public string ReferenciaOxxo { get; set; }
        public string Message { get; set; }
        public string Mensaje { get; set; }
        public string LineaCapturaHSBC { get; set; }
        public string LineaCapturaOtros { get; set; }
        public string Code { get; set; }

        public ResultKiosko()
        {
        }
    }
}
