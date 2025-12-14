using System.Collections.Generic;
using System.ServiceModel;
using SmartFactory.Models; // Tens de ter a referência ao projeto Models adicionada!

namespace SmartFactory.Soap
{
    [ServiceContract]
    public interface IMachineService
    {
        [OperationContract]
        List<SensorData> GetCurrentSensors();

        [OperationContract]
        List<MachineRule> GetAllRules();

        [OperationContract]
        string CreateNewRule(MachineRule newRule);
    }
}