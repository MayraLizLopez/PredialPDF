using Microsoft.AspNetCore.Mvc;
using PredialPdfGenerator.Models;
using Rotativa.AspNetCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace PredialPdfGenerator.Controllers
{
    public class CreatePDF : Controller
    {
        public IActionResult Index()
        {
            
            var datos = PayRepository.GetStatement("10010606101200000");
            var kiosko = PayRepository.GetKiosko("10010606101200000");
            int year = int.Parse(DateTime.Now.ToString("yyyy"));
            var fecha = DateTime.Now;
            var fechaFin = DateTime.Now.AddMonths(1);
            DateTimeFormatInfo formatoFecha = CultureInfo.CurrentCulture.DateTimeFormat;
            int mes = fechaFin.Month;
            int yearFin = fechaFin.Year;
            int diaFin = fecha.Day;
            string nombreMes = "";
            switch (mes)
            {
                case 1:
                    nombreMes = "Enero";
                    break;
                case 2:
                    nombreMes = "Febrero";
                    break;
                case 3:
                    nombreMes = "Marzo";
                    break;
                case 4:
                    nombreMes = "Abril";
                    break;
                case 5:
                    nombreMes = "Mayo";
                    break;
                case 6:
                    nombreMes = "Junio";
                    break;
                case 7:
                    nombreMes = "Julio";
                    break;
                case 8:
                    nombreMes = "Agosto";
                    break;
                case 9:
                    nombreMes = "Septiembre";
                    break;
                case 10:
                    nombreMes = "Octubre";
                    break;
                case 11:
                    nombreMes = "Noviembre";
                    break;
                case 12:
                    nombreMes = "Diciembre";
                    break;
            }

            double corriente = 0, adicional = 0, recargos = 0, rezagos = 0, adicionalrezago = 0, recargosrezago=0,
                multa = 0, ejecucion = 0, descuento = 0, diferenciaconstruccion=0, total = 0;
            for (int i = 0; i < datos.Result.datos.Count; i++)
            {
                if (datos.Result.datos[i].anio == year && datos.Result.datos[i].diferencia_construccion == false)
                {
                    corriente = corriente + datos.Result.datos[i].impuesto_predial;
                    adicional = adicional + datos.Result.datos[i].adicional;
                    recargos = recargos + datos.Result.datos[i].total_recargos;
                }
                if (datos.Result.datos[i].anio < year && datos.Result.datos[i].diferencia_construccion == false)
                {
                    rezagos = rezagos + datos.Result.datos[i].impuesto_predial;
                    adicionalrezago = adicionalrezago + datos.Result.datos[i].adicional;
                    recargosrezago = recargosrezago + datos.Result.datos[i].total_recargos;
                }
                if (datos.Result.datos[i].diferencia_construccion == false)
                {
                    multa = multa + datos.Result.datos[i].multa;
                    ejecucion = ejecucion + datos.Result.datos[i].gastos_ejecuacion;
                    descuento = descuento + datos.Result.datos[i].descuento_predial + datos.Result.datos[i].descuento_recargo + datos.Result.datos[i].descuento_descuento_multa + datos.Result.datos[i].descuento_gasto_ejecucion +
                        datos.Result.datos[i].descuento_autorizado_predial + datos.Result.datos[i].descuento_autorizado_recargo + datos.Result.datos[i].descuento_autorizado_multa + datos.Result.datos[i].descuento_autorizado_gasto_ejecucion;
                }
                if (datos.Result.datos[i].diferencia_construccion == true)
                {
                    diferenciaconstruccion = diferenciaconstruccion + datos.Result.datos[i].impuesto_predial + datos.Result.datos[i].adicional + datos.Result.datos[i].total_recargos + datos.Result.datos[i].multa;
                }
            }

            total = corriente + adicional + recargos + rezagos + adicionalrezago + recargosrezago + multa + ejecucion + diferenciaconstruccion - descuento;

            var datosPredial = new Kiosko()
            {
                Nombre = datos.Result.contribuyente,
                Domicilio = datos.Result.domicilio,
                ClaveCatastral = "10010606101200000",
                Fecha = DateTime.Now.ToString("dd/MM/yyyy"),

                Corriente = "$" + corriente.ToString("N", CultureInfo.InvariantCulture),
                Adicional = "$" + adicional.ToString("N", CultureInfo.InvariantCulture),
                Recargos = "$" + recargos.ToString("N", CultureInfo.InvariantCulture),
                Rezagos = "$" + rezagos.ToString("N", CultureInfo.InvariantCulture),
                AdicionalRezago = "$" + adicionalrezago.ToString("N", CultureInfo.InvariantCulture),
                RecargosRezago = "$" + recargosrezago.ToString("N", CultureInfo.InvariantCulture),
                Multa = "$" + multa.ToString("N", CultureInfo.InvariantCulture),
                Honorario = "$0.00",
                Ejecucion = "$" + ejecucion.ToString("N", CultureInfo.InvariantCulture),
                DiferenciaConstruccion = "$" + diferenciaconstruccion.ToString("N", CultureInfo.InvariantCulture),
                Descuento = "$" + descuento.ToString("N", CultureInfo.InvariantCulture),
                Total = "$" + total.ToString("N", CultureInfo.InvariantCulture),

                LineaCaptura = kiosko.LineaCapturaOtros,
                ReferenciaOxxo = kiosko.ReferenciaOxxo,
                FechaVencimiento = fecha.AddMonths(1).ToString("dd/MM/yyyy"),
                FechaVencimientoTxt = diaFin.ToString() + " de " + nombreMes + " " + yearFin.ToString()
            };



            //return View(datosPredial);
            return new ViewAsPdf("Index", datosPredial) {
                PageSize = Rotativa.AspNetCore.Options.Size.A4,
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Landscape,
                FileName = datosPredial.ClaveCatastral+".pdf"
            };         
        }

    }
}
