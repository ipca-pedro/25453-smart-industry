using System;
using System.Collections.Generic;
using System.Linq; // <--- Necessário para o .ToArray()
using System.ServiceModel;
using SmartFactory.Data;   // <--- Acesso à BD
using SmartFactory.Models; // <--- Acesso aos Modelos

namespace RobotService
{
    public class MachineService : IMachineService
    {
        // Instancia o gestor de base de dados
        private readonly DbManager _db = new DbManager();

        public SensorData[] GetCurrentSensors()
        {
            // Vai buscar os dados REAIS à BD e converte para Array
            return _db.GetLatestReadings().ToArray();
        }

        public MachineRule[] GetAllRules()
        {
            return _db.GetRules().ToArray();
        }

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