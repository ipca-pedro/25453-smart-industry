using System.Collections.Generic;
using SmartFactory.Data;
using SmartFactory.Models;

namespace SmartFactory.Soap
{
    public class MachineService : IMachineService
    {
        private DbManager _db = new DbManager();

        public SensorData[] GetCurrentSensors()
        {
            return _db.GetLatestReadings().ToArray();
        }

        public MachineRule[] GetAllRules()
        {
            return _db.GetRules().ToArray();
        }

        public bool CreateNewRule(MachineRule rule)
        {
            return _db.CreateRule(rule);
        }

        public bool DeleteMachineRule(int id)
        {
            return _db.DeleteRule(id);
        }

        // Implementação da Intervenção Manual com Log
        public bool SetMachinePerformance(int ruleId, double newThreshold, string machineName)
        {
            // O SOAP chama o DbManager que tem a transação e as queries hardcoded
            return _db.ExecuteManualIntervention(ruleId, newThreshold, machineName);
        }
    }
}