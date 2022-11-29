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

class Program
{

    static void Main(string[] args)
    {
        Rootobject results = getData();//must be async
                                       // results = NullearDatos(results);
       /* foreach (var resultados in results.records)
        {
            Console.WriteLine(resultados.dt.Substring(0, 10) + ";" + resultados.dt.Substring(10) + "; " + resultados.fof2);
        }*/
        //Console.WriteLine(results.records[1].dt);
    }

    public static Rootobject NullearDatos(Rootobject obj)
    {
        PropertyInfo[] properties = typeof(Record).GetProperties();
        foreach (var record in obj.records)
        {
            foreach (PropertyInfo property in properties)
            {
                //así obtenemos el valor del atributo
                var Valor = property.GetValue(record);
                if (Valor == null)
                {
                    property.SetValue(record, "NULL");
                }
            }
        }
        
        return obj;
    }
    public static Rootobject getData()//deberia ser async
    {
        Rootobject result = new Rootobject();

        ArrayList jsonFormato = new ArrayList();
        jsonFormato.Add("fecha;hora;fof2");
        //string url = "http://ws-eswua.rm.ingv.it/ais.php/records/wstuj2o_auto?filter=dt,bt,2022-11-05%2022:49:00,2022-11-07%2022:49:00";
        string station = "tuj2o";
        string date_template = "{0}-{1}-{2}";
        Console.WriteLine("Año desde: ");
        int since_year = Int32.Parse(Console.ReadLine());
        Console.WriteLine("Año hasta: ");
        int until_year = Int32.Parse(Console.ReadLine());

        for (int año = since_year; año<= until_year; año++)
        {
            for (int mes = 8; mes <= 10; mes++)
            {
                string since_date = String.Format(date_template, año, mes, 1);//YYYY-MM-DD
                string until_date = String.Format(date_template, año, mes, 1);
                string  since_hour = "00:00:00";//HH:MM:SS
                string until_hour = "23:00:00";
                string url = String.Format("http://ws-eswua.rm.ingv.it/ais.php/records/{0}_auto?filter=dt,bt,{1}%20{2},{3}%20{4}&include+dt,{5}&order=dt", station, since_date, since_hour, until_date, until_hour, station);
                Console.WriteLine("Consultando...");
                var client = new RestClient(url);
                var request = new RestRequest();
                var response = client.Execute(request);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string rawResponse = response.Content;
                    result = JsonConvert.DeserializeObject<Rootobject>(rawResponse);

                    foreach (var record in result.records)
                    {
                        //Guardar en DB
                        Console.WriteLine(record.dt.Substring(0, 10) + ";" + record.dt.Substring(10) + "; " + record.fof2);
                    }
                }
            }
        }
        return result;
    }

}



public class Rootobject
{
    public Record[] records { get; set; }
}

public class Record
{
    public string dt { get; set; }
    public string station { get; set; }
    public string fromfile { get; set; }
    public string producer { get; set; }
    public object evaluated { get; set; }
    public string? fof2 { get; set; }
    public bool fof2_eval { get; set; }
    public string muf3000f2 { get; set; }
    public bool muf3000f2_eval { get; set; }
    public string m3000f2 { get; set; }
    public bool m3000f2_eval { get; set; }
    public string fxi { get; set; }
    public bool fxi_eval { get; set; }
    public string fof1 { get; set; }
    public bool fof1_eval { get; set; }
    public string ftes { get; set; }
    public bool ftes_eval { get; set; }
    public int? h_es { get; set; }
    public bool h_es_eval { get; set; }
    public object aip_hmf2 { get; set; }
    public object aip_fof2 { get; set; }
    public object aip_fof1 { get; set; }
    public object aip_hmf1 { get; set; }
    public object aip_d1 { get; set; }
    public object aip_foe { get; set; }
    public object aip_hme { get; set; }
    public object aip_yme { get; set; }
    public object aip_h_ve { get; set; }
    public object aip_ewidth { get; set; }
    public object aip_deln_ve { get; set; }
    public object aip_b0 { get; set; }
    public object aip_b1 { get; set; }
    public object tec_bottom { get; set; }
    public object tec_top { get; set; }
    public object profile { get; set; }
    public object trace { get; set; }
    public string modified { get; set; }
    public string fof2_med_27_days { get; set; }
}


