// See https://aka.ms/new-console-template for more information
using RestSharp;
using System.Collections;
using System.Net;
using Newtonsoft.Json;
using System.Runtime.InteropServices;
using System.Reflection.Metadata;
using System.Data.Common;
using System;
using System.Reflection;
using Newtonsoft.Json.Linq;
using Ionosfera;

class Program
{

    static void Main(string[] args)
    {
        Rootobject results = new();
        GetData();//must be async
        Console.WriteLine(results.ToString());
                                                  // results = NullearDatos(results);
        /* foreach (var resultados in results.records)
         {
             Console.WriteLine(resultados.dt.Substring(0, 10) + ";" + resultados.dt.Substring(10) + "; " + resultados.fof2);
         }*/
        //Console.WriteLine(results.records[1].dt);
    }

    public static Rootobject GetData()
    {
        var respuesta = new Rootobject();
        //preparacion de los datos para la consulta
        ArrayList jsonFormato = new ArrayList();
        jsonFormato.Add("fecha;hora;fof2");
        string station = "tuj2o";
        string date_template = "{0}-{1}-{2}";
        Console.WriteLine("Año desde: ");
        int since_year = Int32.Parse(Console.ReadLine());
        Console.WriteLine("Año hasta: ");
        int until_year = Int32.Parse(Console.ReadLine());


        //loop de las consultas
        for (int año = since_year; año <= until_year; año++)
        {
            for (int mes = 1; mes <= 12; mes++)
            {
                string since_date = String.Format(date_template, año, mes, 1);//YYYY-MM-DD
                string until_date = String.Format(date_template, año, mes, 1);
                string since_hour = "00:00:00";//HH:MM:SS
                string until_hour = "23:00:00";
                string url = String.Format("http://ws-eswua.rm.ingv.it/ais.php/records/{0}_auto?filter=dt,bt,{1}%20{2},{3}%20{4}&include+dt,{5}&order=dt", station, since_date, since_hour, until_date, until_hour, station);
                Console.WriteLine("Consultando...");

                var client = new RestClient(url);
                var request = new RestRequest();
                var response = client.Execute(request);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string rawResponse = response.Content;
                    respuesta = JsonConvert.DeserializeObject<Rootobject>(rawResponse);

                    foreach (var record in respuesta.Records)
                    {
                        //Guardar en DB
                        Console.WriteLine(record.dt.Substring(0, 10) + ";" + record.dt.Substring(10) + "; " + record.fof2);
                    }
                }
            }
        }
        return respuesta;
    }
}

    

