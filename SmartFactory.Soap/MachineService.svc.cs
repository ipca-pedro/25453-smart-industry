using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
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

        // SEM async, SEM Task, SEM await
        public string CreateNewRule(MachineRule newRule)
        {
            try
            {
                return _db.CreateRule(newRule);
            }
            catch (Exception ex)
            {
                return "Erro: " + ex.Message;
            }
        }
    }
}