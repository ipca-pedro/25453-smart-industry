using System;
using System.Runtime.Serialization;

namespace SmartFactory.Models
{
    public class MachineRule
    {
        public int Id { get; set; }
        public string TargetSensorId { get; set; } // Coincide com 'target_sensor_id'
        public string RuleName { get; set; }       // Coincide com 'rule_name'
        public double ThresholdValue { get; set; } // Coincide com 'threshold_value'
        public string ConditionType { get; set; }  // Coincide com 'condition_type'
        public string ActionCommand { get; set; }  // Coincide com 'action_command'
        public bool IsActive { get; set; }         // Coincide com 'is_active'
    }
}