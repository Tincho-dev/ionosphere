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
        //results = ConsultaUnDia();//must be async 
        Console.WriteLine(results.ToString());
        //Console.WriteLine(results.Records.Length);
    }

    public static Rootobject ConsultaUnDia()//error en la consulta, posiblemente mal escrita la url
    {
        var respuesta = new Rootobject();

        //preparacion de los datos para la consulta
        ArrayList jsonFormato = new ArrayList();
        jsonFormato.Add("fecha;hora;fof2");
        string station = "tuj2o";
        string date_template = "{0}-{1}-{2}";
        Console.WriteLine("Año: ");
        int year = Int32.Parse(Console.ReadLine());
        Console.WriteLine("mes: ");
        int month = Int32.Parse(Console.ReadLine());
        Console.WriteLine("dia: ");
        int day = Int32.Parse(Console.ReadLine());


        //loop de las consultas
        
                string since_date = String.Format(date_template, year, month, day);//YYYY-MM-DD
                string since_hour = "00:00:00";//HH:MM:SS
                string until_hour = "23:00:00";
                string url = String.Format("http://ws-eswua.rm.ingv.it/ais.php/records/{0}_auto?filter=dt,{1}%20{2}&include+dt,{3}&order=dt", station, since_date, since_hour, station);
                Console.WriteLine("Consultando...");

                var client = new RestClient(url);
                var request = new RestRequest();
                var response = client.Execute(request);
                
                Console.WriteLine(response.StatusCode);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string rawResponse = response.Content;
                    respuesta = JsonConvert.DeserializeObject<Rootobject>(rawResponse);

                    foreach (var record in respuesta.Records)
                    {
                        //Guardar en DB
                        Console.WriteLine(record.dt.Substring(0, 10) + ";" + record.dt.Substring(10) + "; " + record.fof2);
                    }
                }
        return respuesta;
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

                if (response.StatusCode == HttpStatusCode.OK)
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

    

