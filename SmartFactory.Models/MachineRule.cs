using System;
using System.Runtime.Serialization;

namespace SmartFactory.Models
{
    [DataContract]
    public class MachineRule
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string TargetSensorId { get; set; }

        [DataMember]
        public string RuleName { get; set; }

        [DataMember]
        public double ThresholdValue { get; set; }

        [DataMember]
        public string ConditionType { get; set; } // ">" ou "<"

        [DataMember]
        public string ActionCommand { get; set; } // Ex: "BAIXAR_PERFORMANCE"

        [DataMember]
        public bool IsActive { get; set; }
    }
}