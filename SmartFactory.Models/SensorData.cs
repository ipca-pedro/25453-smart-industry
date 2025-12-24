using System;

namespace SmartFactory.Models
{
    public class SensorData
    {
        public string SensorId { get; set; } // Coluna 'sensor'
        public string Polo { get; set; }     // Coluna 'Polo'
        public double Valor { get; set; }    // Coluna 'Valor'
        public string Unidade { get; set; }  // Coluna 'Unidade'
        public DateTime DataHora { get; set; } // Coluna 'DataHora'
    }
}