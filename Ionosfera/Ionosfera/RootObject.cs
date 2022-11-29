using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Ionosfera
{
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
    public class Rootobject
    {
        public Record[]? Records { get; set; }

        public void NullearDatos()
        {
            PropertyInfo[] properties = typeof(Record).GetProperties();
            foreach (var record in Records)
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
        }

    }
    

}

