using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace RobotService
{
    // --- COLE A CLASSE DE IMPLEMENTAÇÃO AQUI ---
    public class MachineService : IMachineService
    {
        public List<SensorData> GetCurrentSensors()
        {
            return new List<SensorData>
            {
                new SensorData { Id = 1, Tipo = "Temp", Valor = 45.5 }
            };
        }

        public List<MachineRule> GetAllRules()
        {
            return new List<MachineRule>();
        }

        public string CreateNewRule(MachineRule newRule)
        {
            return "Regra criada com sucesso";
        }
    }
}