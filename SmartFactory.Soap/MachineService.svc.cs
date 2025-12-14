using System.Collections.Generic;
using SmartFactory.Data;   // Tens de ter a referência ao projeto Data adicionada!
using SmartFactory.Models; // Tens de ter a referência ao projeto Models adicionada!

namespace SmartFactory.Soap
{
    public class MachineService : IMachineService
    {
        private readonly DbManager _db;

        public MachineService()
        {
            _db = new DbManager();
        }

        public List<SensorData> GetCurrentSensors()
        {
            return _db.GetLatestReadings();
        }

        public List<MachineRule> GetAllRules()
        {
            return _db.GetRules();
        }

        public string CreateNewRule(MachineRule newRule)
        {
            return _db.CreateRule(newRule);
        }
    }
}