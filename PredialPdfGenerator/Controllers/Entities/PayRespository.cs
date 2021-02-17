using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using PredialPdfGenerator.Models.Entities;
using PredialPdfGenerator.Models;

namespace PredialPdfGenerator.Controllers
{
    public class PayRepository
    {
        public static object ConfigurationManager { get; private set; }

        //Método que permite consultar los datos de predial en caso de ser deudor o no
        public static async Task<ResponseStatement> GetStatement(string predialId)
        {
            string baseURL = "http://192.168.0.5/";

            string resource = "api/predial/estado/";
            string queryString = "?name=04197" + predialId + "";
            Uri requestUri = new Uri(baseURL + resource + queryString);
            HttpWebRequest req = WebRequest.Create(requestUri) as HttpWebRequest;

            req.Headers["Access-Token"] = "a1a09df74a235deee6db95a9511a728fc9812dd2";
            req.ContentType = "text/html; charset=utf-8";

            var res = "";

            ResponseStatement responseStatement = new ResponseStatement();
            try
            {
                using (HttpWebResponse response = (HttpWebResponse)req.GetResponse())
                {
                    using (StreamReader sr = new StreamReader(response.GetResponseStream()))
                    {
                        res = await sr.ReadToEndAsync();
                        responseStatement = JsonConvert.DeserializeObject<ResponseStatement>(res);

                        if (responseStatement.error == "604")
                            responseStatement.error = "false";
                    }
                }
            }
            catch (Exception ex)
            {
                responseStatement.error = "true";
                responseStatement.mensaje = ex.Message;

            }



            GetHeaderStatement(predialId, responseStatement);


            
            return responseStatement;


        }

        //Método que permite obtener datos del predio como son:
        //Nombnre del ciudadano(a), dirección, tipo de persona,
        //Clave catastral original, valor del terreno, y valor de la construcción
        public static async void GetHeaderStatement(string predialId, ResponseStatement responseStatement)
        {
            string baseURL = "http://192.168.0.5/";

            string resource = "api/predial/busqueda/";
            string queryString = "?name=04197" + predialId + "";
            Uri requestUri = new Uri(baseURL + resource + queryString);
            HttpWebRequest req = WebRequest.Create(requestUri) as HttpWebRequest;

            req.Headers["Access-Token"] = "a1a09df74a235deee6db95a9511a728fc9812dd2";
            req.ContentType = "text/html; charset=utf-8";

            var res = "";
            ResponseInfoPredio responseInfoPredio = new ResponseInfoPredio();

            try
            {
                using (HttpWebResponse response = (HttpWebResponse)req.GetResponse())
                {
                    using (StreamReader sr = new StreamReader(response.GetResponseStream()))
                    {
                        res = await sr.ReadToEndAsync();
                        responseInfoPredio = JsonConvert.DeserializeObject<ResponseInfoPredio>(res);
                        if (responseInfoPredio.datos.Count != 0)
                            foreach (var item in responseInfoPredio.datos)
                            {
                                foreach (var prop in item.propietarios)
                                {
                                    if (prop.titular == true)
                                    {
                                        responseStatement.domicilio = item.domicilio;
                                        if (item.propietarios.Count != 0)
                                        {
                                            if (prop.apellido_paterno == "")
                                                prop.apellido_paterno = " ";
                                            if (prop.apellido_materno == "")
                                                prop.apellido_materno = " ";
                                            if (item.domicilio == null)
                                                item.domicilio = " ";
                                            responseStatement.apellido_paterno = prop.apellido_paterno.Substring(0, 1) + "*** ";
                                            responseStatement.apellido_materno = prop.apellido_materno.Substring(0, 1) + "***";
                                            responseStatement.nombre = prop.nombre.Substring(0, 1) + "*** " + responseStatement.apellido_paterno + responseStatement.apellido_materno;
                                            responseStatement.contribuyente = prop.nombre + " " + prop.apellido_paterno + " " + prop.apellido_materno;
                                        }
                                        else
                                        {
                                            responseStatement.apellido_paterno = "";
                                            responseStatement.apellido_materno = "";
                                            responseStatement.nombre = "";
                                        }
                                        responseStatement.valor_construccion = item.valor_construccion;
                                        responseStatement.valor_terreno = item.valor_terreno;
                                        responseStatement.valor_catastral = responseStatement.valor_construccion + responseStatement.valor_terreno;

                                    }
                                }

                            }
                        else
                        {
                            responseStatement.nombre = "La clave predial proporcionada no ha sido encontrada, favor de verificar. ";
                        }


                    }
                }
            }
            catch (Exception ex)
            {
                responseStatement.error = "true";
                responseStatement.mensaje = ex.Message;

                ErrorDescription miError = new ErrorDescription();
                miError.mensaje = responseInfoPredio.error_descrip.mensaje;
                miError.excepcion = false;
                miError.error = true;

                responseStatement.error_descrip = miError;
            }
        }

        //Método que permite realizar la busqueda del pase de tesorería 
        public static TreasuryPass GetPassPredial(string amount, string predialId)
        {

            string baseURL = "http://192.168.0.5/";

            string resource = "api/predial/pase/";
            string queryString = "?name=04197" + predialId + "&convenio=0001";
            Uri requestUri = new Uri(baseURL + resource + queryString);
            HttpWebRequest req = WebRequest.Create(requestUri) as HttpWebRequest;
            req.Method = "POST";
            req.Headers["Access-Token"] = "a1a09df74a235deee6db95a9511a728fc9812dd2";
            req.ContentType = "text/html; charset=utf-8";

            var res = "";
            TreasuryPass result = new TreasuryPass();
            try
            {
                using (HttpWebResponse response = (HttpWebResponse)req.GetResponse())
                {
                    using (StreamReader sr = new StreamReader(response.GetResponseStream()))
                    {
                        res = sr.ReadToEnd();
                        result = JsonConvert.DeserializeObject<TreasuryPass>(res);


                    }
                }
            }
            catch (Exception ex)
            {
                result.Error = ex.Message;

            }

            return result;
        }


        //Método que realiza la consulta para obtener las líneas de captura para pago referenciado
        //Retorna la dirección, nombre del ciudadano(a), tipo de persona
        // clave catastral, valor del terreno, valor de la construcción,
        //Linea de captura bancos y Referencia oxxo
        public static ResultKiosko GetKiosko(string predialId)
        {
            string baseURL = "http://192.168.0.5/";

            string resource = "api/ref/generar";
            string queryString = "?cveCatastral=04197" + predialId + "";
            Uri requestUri = new Uri(baseURL + resource + queryString);
            HttpWebRequest req = WebRequest.Create(requestUri) as HttpWebRequest;

            req.Headers["Access-Token"] = "a1a09df74a235deee6db95a9511a728fc9812dd2";
            req.ContentType = "text/html; charset=utf-8";

            var res = "";

            ResultKiosko responseStatement = new ResultKiosko();
            try
            {
                using (HttpWebResponse response = (HttpWebResponse)req.GetResponse())
                {
                    using (StreamReader sr = new StreamReader(response.GetResponseStream()))
                    {
                        res = sr.ReadToEnd();
                        responseStatement = JsonConvert.DeserializeObject<ResultKiosko>(res);

                        if (responseStatement.Error == "604")
                            responseStatement.Error = "false";
                    }
                }
            }
            catch (Exception ex)
            {
                responseStatement.Error = "true";
                responseStatement.Mensaje = ex.Message;

            }
            return responseStatement;
        }

    }
}
