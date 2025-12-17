using System.ServiceModel;
using SmartFactory.Models;

namespace RobotService
{
    [ServiceContract]
    public interface IMachineService
    {
        // Ler Sensores
        [OperationContract]
        SensorData[] GetCurrentSensors();

        // --- CRUD DE REGRAS ---

        [OperationContract]
        MachineRule[] GetAllRules();

        [OperationContract]
        string CreateNewRule(MachineRule newRule);

        [OperationContract]
        string UpdateMachineRule(int ruleId, double limite, string descricao);

        [OperationContract]
        string DeleteMachineRule(int ruleId);
    }
}