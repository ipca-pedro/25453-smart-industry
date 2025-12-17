using System;
using System.Linq;
using SmartFactory.Data;
using SmartFactory.Models;

namespace RobotService
{
    public class MachineService : IMachineService
    {
        private readonly DbManager _db = new DbManager();

        public SensorData[] GetCurrentSensors()
        {
            return _db.GetLatestReadings().ToArray();
        }

        public MachineRule[] GetAllRules()
        {
            return _db.GetRules().ToArray();
        }

        public string CreateNewRule(MachineRule newRule)
        {
            return _db.CreateRule(newRule);
        }

        public string UpdateMachineRule(int ruleId, double limite, string descricao)
        {
            return _db.UpdateRule(ruleId, limite, descricao);
        }

        public string DeleteMachineRule(int ruleId)
        {
            return _db.DeleteRule(ruleId);
        }
    }
}