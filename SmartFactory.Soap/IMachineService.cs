using System.Collections.Generic;
using System.ServiceModel;
using SmartFactory.Models;

namespace SmartFactory.Soap
{
    [ServiceContract]
    public interface IMachineService
    {
        // --- Leituras ---
        [OperationContract]
        SensorData[] GetCurrentSensors();

        // --- CRUD de Regras ---
        [OperationContract]
        MachineRule[] GetAllRules();

        [OperationContract]
        bool CreateNewRule(MachineRule rule);

        [OperationContract]
        bool DeleteMachineRule(int id);

        // Este método permite alterar o threshold e gravar o log numa só transação
        [OperationContract]
        bool SetMachinePerformance(int ruleId, double newThreshold, string machineName);
    }
}