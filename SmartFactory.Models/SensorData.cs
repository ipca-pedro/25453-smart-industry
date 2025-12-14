using System;
using System.Runtime.Serialization; // Essencial para o SOAP funcionar

namespace SmartFactory.Models
{
    [DataContract]
    public class SensorData
    {
        [DataMember]
        public string SensorId { get; set; }

        [DataMember]
        public string Polo { get; set; }

        [DataMember]
        public double Valor { get; set; }

        [DataMember]
        public string Unidade { get; set; }

        [DataMember]
        public DateTime DataHora { get; set; }
    }
}