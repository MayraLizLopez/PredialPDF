using System;
namespace PredialPdfGenerator.Models.Entities
{
    public class TreasuryPass
    {
        public string Control_Number { get; set; }
        public double Monto { get; set; }
        public string Error { get; set; }

        public TreasuryPass()
        {
        }
    }
}
